using AutoMapper;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using TestHotelSmart.WebApi.Application.Contracts;
using TestHotelSmart.WebApi.DataAccess.Repositories.Contractos;
using TestHotelSmart.WebApi.DTO.Hotel;
using TestHotelSmart.WebApi.DTO.Reservation;
using TestHotelSmart.WebApi.DTO.Response;
using TestHotelSmart.WebApi.DTO.Room;
using TestHotelSmart.WebApi.Model;
using MailKit.Net.Smtp;

namespace TestHotelSmart.WebApi.Application
{
    class ClientApplication :IClientApplication
    {
        #region Fields
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper mapper;
        protected readonly TestHotelContext _context;
        #endregion

        #region Constants

        private string FROM = "danivega1986@gmail.com";
        private string TOKEN = "lots yuni iory ktkb";

        #endregion

        #region Builders

        public ClientApplication(IHotelRepository hotelRepository,
                IMapper mapper, TestHotelContext context, IRoomRepository roomRepository,
                IReservationRepository reservationRepository, IGuestRepository guestRepository,
                ICustomerRepository customerRepository)
        {
            _hotelRepository = hotelRepository;
            this.mapper = mapper;
            _context = context;
            _roomRepository = roomRepository;
            _reservationRepository = reservationRepository;
            _guestRepository = guestRepository;
            _customerRepository = customerRepository;
        }
        #endregion

        #region Methods

        public ResponseQuery<List<HotelDTO>> GetRoomsAvailable(SearchHotelAndRoomDTO search)
        {
            ResponseQuery<List<HotelDTO>> response = new();
            try
            {
                #region Buscar habitaciones activas de hoteles activos
                City? city = _context.City.FirstOrDefault(x => x.CityName.Equals(search.CityName));
                if (city == null)
                {
                    response.ResponseMessage("CityName: La ciudad seleccionada no se encuentra registrada.", false);
                    return response;
                }

                var hotelsList = _hotelRepository.List().Where(x => x.IsActive && 
                                            x.IdCityNavigation.CityName.Equals(search.CityName));

                List<Hotel>? hotelsWithActiveRooms = hotelsList
                    .Select(hotel => new
                    {
                        Hotel = hotel,
                        ActiveRooms = hotel.Room.Where(room => room.IsActive).ToList()
                    })
                    .Where(h => h.ActiveRooms.Any())  // Filtrar hoteles que no tienen habitaciones activas
                    .Select(h =>
                        {
                            var hotel = h.Hotel;
                            hotel.Room = h.ActiveRooms; // Reemplazar las habitaciones con solo las activas
                            return hotel;
                        })
                    .ToList();

                var hotelsDTOList = mapper.Map<List<HotelDTO>>(hotelsWithActiveRooms);

                #endregion

                #region Buscar por fechas en las reservaciones

                // Obtener los IDs de las habitaciones activas
                var activeRoomIds = hotelsWithActiveRooms
                    .SelectMany(hotel => hotel.Room)
                    .Select(room => room.Id)
                    .ToList();

                // Filtrar las reservaciones que correspondan a las habitaciones activas
                var activeReservations = _reservationRepository.List()
                    .Where(res => activeRoomIds.Contains(res.IdRoom) 
                            && ( search.ArrivalDate >= res.ArrivalDate || search.DepartureDate <= res.DepartureDate))
                    .ToList();

                var reservedRoomIds = activeReservations.Select(res => res.IdRoom).ToList();
                var filteredHotelsWithActiveRooms = hotelsWithActiveRooms
                    .Select(hotel => new Hotel
                    {
                        Id = hotel.Id,
                        HotelName = hotel.HotelName,
                        IdCity = hotel.IdCityNavigation.Id,
                        IdCityNavigation = hotel.IdCityNavigation,
                        IsActive = hotel.IsActive,
                        Room = hotel.Room.Where(room => !reservedRoomIds.Contains(room.Id)).ToList()
                    })
                    .Where(h => h.Room.Any())  // Filtrar hoteles que no tienen habitaciones disponibles después del filtro
                    .ToList();

                var hotelsDTOReturn = mapper.Map<List<HotelDTO>>(filteredHotelsWithActiveRooms);

                #endregion


                response.Result = hotelsDTOReturn.ToList();

            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }

        public ResponseQuery<bool> CreateReservation(CreateReservationDTO request)
        {
            ResponseQuery<bool> response = new();
            try
            {
                #region Validaciones de estructura

                if (request.Quantity > 1 && request.Guests == null)
                    response.ResponseMessage("Guest: La reserva es para mas de una persona, agregue la lista de faltantes.", false);

                #endregion

                #region Validacion de Data

                List<Gender> genders = _context.Gender.ToList();
                List<Documenttype> documentTypes = _context.Documenttype.ToList();

                Gender? genderPrincipalPerson = genders.FirstOrDefault(x => x.GenderDescription.Equals(request.GenderDescription));
                if (genderPrincipalPerson == null)
                {
                    response.ResponseMessage("GenderDescription: La descripción del genero no existe en la base de datos.", false);
                    return response;
                }

                Documenttype? documentPrincipalPerson = documentTypes.FirstOrDefault(x => x.DocumentDescription.Equals(request.DocumentDescription));
                if (documentPrincipalPerson == null)
                {
                    response.ResponseMessage("DocumentDescription: La descripción del documento no existe en la base de datos.", false);
                    return response;
                }

                Reservation reservation = new Reservation
                                             {
                                                DocumentNumber = request.DocumentNumber,
                                                IdRoom = request.Id,
                                                ArrivalDate = request.ArrivalDate,
                                                DepartureDate = request.DepartureDate,
                                                Quantity = request.Quantity,
                                                EmergencyContactFullName = request.EmergencyContactFullName,
                                                EmergencyContactPhoneNumber = request.EmergencyContactPhoneNumber
                                             };

                List<Customer> newCustomers = new();
                List<Guest> newGuests = new();

                Customer? customerHeaderExists = _customerRepository.GetCustomerByDocument(request.DocumentNumber);
                if (customerHeaderExists == null)
                    newCustomers.Add(ExtractGuestFromHeader(request, genderPrincipalPerson.Id, documentPrincipalPerson.Id));

                Guest principalGuestAdded = new Guest
                {
                    DocumentNumber = request.DocumentNumber,
                    IdReservation = reservation.Id
                };

                newGuests.Add(principalGuestAdded);

                List<string> errors = new();
                if (request.Guests != null && request.Guests.Count > 0)
                {
                    List<string> documentnumbers = request.Guests.Select(x => x.DocumentNumber).ToList();
                    List<Customer>? customersExists = _customerRepository.GetCustomersByDocument(documentnumbers);
                    int i = 0;
                    foreach (var guest in request.Guests)
                    {
                        if (!customersExists.Any(x => x.DocumentNumber.Equals(guest.DocumentNumber)))
                        {
                            Gender? genderPerson = genders.FirstOrDefault(x => x.GenderDescription.Equals(request.GenderDescription));
                            if (genderPerson == null)
                                errors.Add($"Guests{i + 1}:GenderDescription: La descripción del genero no existe en la base de datos.");

                            Documenttype? documentPerson = documentTypes.FirstOrDefault(x => x.DocumentDescription.Equals(request.DocumentDescription));
                            if (documentPerson == null)
                                errors.Add($"Guests{i + 1}:DocumentDescription: La descripción del documento no existe en la base de datos.");

                            if (!errors.Any()) 
                            {
                                Customer newCustomer = mapper.Map<Customer>(guest);
                                newCustomer.IdDocumentType = Convert.ToInt32(documentPerson?.Id);
                                newCustomer.IdGender = Convert.ToInt32(genderPerson?.Id);
                                newCustomers.Add(newCustomer);
                            }
                                
                        }

                        Guest guestAdded = new Guest
                        {
                            DocumentNumber = guest.DocumentNumber,
                            IdReservation = reservation.Id
                        };

                        newGuests.Add(guestAdded);

                        i++;
                    }

                }

                if (errors.Count > 0)
                {
                    response.ResponseMessage(string.Join(",", errors), false);
                    return response;
                }

                #endregion
                if (newCustomers.Count > 0)
                    _customerRepository.InsertTransactList(newCustomers);

                reservation.Guest = newGuests;
                _reservationRepository.InsertTransact(reservation);

                _context.SaveChanges();

                #region send Email

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse($"from_{FROM}"));
                email.To.Add(MailboxAddress.Parse($"{FROM}"));
                email.Subject = "Confirmación de reserva";
                email.Body = new TextPart(TextFormat.Plain) { Text = $"Se ha Creado una reserva a nombre de {request.FullName} entre las fechas {request.ArrivalDate} y {request.DepartureDate}" };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate($"{FROM}", $"{TOKEN}");
                smtp.Send(email);
                smtp.Disconnect(true);

                #endregion
            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }

        public static Customer ExtractGuestFromHeader(CreateReservationDTO reservation, int idGender, int idDocument)
        {
            return new Customer
            {
                DocumentNumber = reservation.DocumentNumber,
                FullName = reservation.FullName,
                IdGender = idGender,
                IdDocumentType = idDocument,
                Email = reservation.Email,
                PhoneNumber = reservation.PhoneNumber
            };
        }

        #endregion



    }
}
