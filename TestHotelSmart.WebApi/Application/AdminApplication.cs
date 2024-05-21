using AutoMapper;
using TestHotelSmart.WebApi.DTO.Response;
using TestHotelSmart.WebApi.Application.Contracts;
using TestHotelSmart.WebApi.DataAccess.Repositories.Contractos;
using TestHotelSmart.WebApi.DTO.Hotel;
using TestHotelSmart.WebApi.Model;
using System.Diagnostics.Metrics;
using TestHotelSmart.WebApi.DTO.Room;
using TestHotelSmart.WebApi.DTO.Reservation;
using TestHotelSmart.WebApi.DTO.Customer;

namespace TestHotelSmart.WebApi.Application
{
    class AdminApplication : IAdminApplication
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

        #region Builders


        public AdminApplication(IHotelRepository hotelRepository, 
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

        public ResponseQuery<List<HotelDTO>> GetHotels()
        {
            ResponseQuery<List<HotelDTO>> response = new();
            try
            {
                var hotelsList = _hotelRepository.List();


                var hotelsDTOList = mapper.Map<List<HotelDTO>>(hotelsList);
                response.Result = hotelsDTOList.ToList();

            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }


        public ResponseQuery<List<HotelDTO>> GetHotelsActive()
        {
            ResponseQuery<List<HotelDTO>> response = new();
            try
            {
                var hotelsList = _hotelRepository.List().Where(x => x.IsActive);


                var hotelsDTOList = mapper.Map<List<HotelDTO>>(hotelsList);
                response.Result = hotelsDTOList.ToList();

            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }

        public ResponseQuery<bool> CreateHotel(HotelDTO request)
        {
            ResponseQuery<bool> response = new();
            try
            {
                #region Validaciones de estructura

                var errors = ValidateHotelDTO(request);
                if (errors.Count > 0)
                {
                    response.ResponseMessage(string.Join(",", errors), false);
                    return response;
                }

                #endregion

                #region Validacion de Data

                var hotel = mapper.Map<Hotel>(request);
                hotel.Room = mapper.Map<List<Room>>(request.Rooms);
                int? idCity = _context.City.SingleOrDefault(x => x.CityName.Equals(request.CityName))?.Id;

                if (idCity != null)
                    hotel.IdCity = Convert.ToInt32(idCity);
                else
                    errors.Add("El campo CityName No existen en la Base de Datos.");

                if (hotel.Room.Count > 0)
                {
                    List<Roomtype> roomTypeList = _context.Roomtype.ToList();
                    int i = 0;
                    foreach (var room in hotel.Room)
                    {
                        room.IdHotel = hotel.Id;
                        int? roomTypeId = roomTypeList.SingleOrDefault(x => x.RoomDescription.Equals(request.Rooms?[i].RoomDescription))?.Id;

                        if (roomTypeId == null)
                            errors.Add($"Room {i + 1}: Campo RoomDescription: no existe en la base de datos.");
                        else
                            room.IdRoomType = Convert.ToInt32(roomTypeId);
                        i++;
                    }

                }

                if (errors.Count > 0)
                {
                    response.ResponseMessage(string.Join(",", errors), false);
                    return response;
                }

                #endregion

                if (hotel.Id == 0)
                    _hotelRepository.Insert(hotel);
                else
                    _hotelRepository.Update(hotel);

            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }

        public ResponseQuery<bool> CreateRooms(CreateRoomDTO request)
        {
            ResponseQuery<bool> response = new();
            try
            {
                #region Validaciones de estructura

                if (request.Rooms == null)
                {
                    response.ResponseMessage("Se debe enviar por lo menos una habitacion.", false);
                    return response;
                }

                if (string.IsNullOrEmpty(request.CityName))
                {
                    response.ResponseMessage("CityName: Se debe enviar el nombre de la ciudad.", false);
                    return response;
                }

                if (string.IsNullOrEmpty(request.HotelName))
                {
                    response.ResponseMessage("HotelName: Se debe enviar el nombre del hotel al que se le van a crear las habitaciones.", false);
                    return response;
                }

                var errors = ValidateRoomDTO(request.Rooms);
                if (errors.Count > 0)
                {
                    response.ResponseMessage(string.Join(",", errors), false);
                    return response;
                }

                #endregion

                #region Validacion de Data

                City? city = _context.City.FirstOrDefault(x => x.CityName.Equals(request.CityName));

                if (city == null)
                {
                    response.ResponseMessage("CityName: La ciudad seleccionada no existe en la base de datos.", false);
                    return response;
                }

                Hotel? hotel = _hotelRepository.GetHotelByCityAndName(request.HotelName, city.Id);

                if (hotel == null)
                {
                    response.ResponseMessage("HotelName: El hotel seleccionado no existe en la base de datos o no esta relacionado a esa ciudad.", false);
                    return response;
                }

                var rooms = mapper.Map<List<Room>>(request.Rooms);
                List<Roomtype> roomTypeList = _context.Roomtype.ToList();
                for (int i = 0; i < rooms.Count; i++)
                {
                    rooms[i].IdHotel = hotel.Id;
                    int? roomTypeId = roomTypeList.SingleOrDefault(x => x.RoomDescription.Equals(request.Rooms[i].RoomDescription))?.Id;

                    if (roomTypeId == null)
                        errors.Add($"Room {i + 1}: Campo RoomDescription: no existe en la base de datos.");
                    else
                        rooms[i].IdRoomType = Convert.ToInt32(roomTypeId);
                }

                if (errors.Count > 0)
                {
                    response.ResponseMessage(string.Join(",", errors), false);
                    return response;
                }

                #endregion

                _roomRepository.InsertList(rooms);
                
            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }

        public ResponseQuery<bool> UpdateHotel(UpdateHotelDTO request)
        {
            ResponseQuery<bool> response = new();
            try
            {
                #region Validaciones de estructura

                var errors = ValidateUpdateHotelDTO(request);
                if (errors.Count > 0)
                {
                    response.ResponseMessage(string.Join(",", errors), false);
                    return response;
                }

                #endregion

                #region Validacion de Data

                int? idCity = _context.City.SingleOrDefault(x => x.CityName.Equals(request.CityName))?.Id;
                if (idCity == null)
                    errors.Add("El campo CityName No existen en la Base de Datos.");
                    
                Hotel? hotel = _hotelRepository.GetHotel(request.Id);
                if (hotel == null)
                    response.ResponseMessage("El Hotel no se encuentra registrado.", false);
                else {
                    hotel.IsActive = request.IsActive;
                    hotel.HotelName = request.HotelName;
                    hotel.IdCity = Convert.ToInt32(idCity);

                    _hotelRepository.Update(hotel);
                }
                
                #endregion

            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }


        public ResponseQuery<bool> UpdateRoom(RoomDTO request)
        {
            ResponseQuery<bool> response = new();
            try
            {
                #region Validaciones de estructura

                var errors = ValidateUpdateRoomDTO(request);
                if (errors.Count > 0)
                {
                    response.ResponseMessage(string.Join(",", errors), false);
                    return response;
                }

                #endregion

                #region Validacion de Data

                Room? room = _roomRepository.GetRoom(Convert.ToInt32(request.Id));

                if (room == null)
                {
                    response.ResponseMessage("La habitación seleccionado no existe en la base de datos", false);
                    return response;
                }

                Roomtype? roomType = _context.Roomtype.FirstOrDefault(x => x.RoomDescription.Equals(request.RoomDescription));

                if (roomType == null)
                {
                    response.ResponseMessage("RoomDescription: El tipo de Habitación no existe en la base de datos.", false);
                    return response;
                }
                else
                    room.IdRoomType = Convert.ToInt32(roomType.Id);

                room.Taxes = request.Taxes;
                room.IsActive = request.IsActive;
                room.Price = request.Price;
                room.RoomName = request.RoomName;

                #endregion

                _roomRepository.Update(room);

            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }

        public ResponseQuery<List<ReservationDTO>> GetReservations()
        {
            ResponseQuery<List<ReservationDTO>> response = new();
            try
            {
                var reservationList = _reservationRepository.List();
                List<ReservationDTO> reservationDTOList = new();
                foreach (var reservation in reservationList)
                {
                    List<string> GuestDocumentNumber = _guestRepository.GetGuestsByReservation(reservation.Id);
                    var reservationDTO = mapper.Map<ReservationDTO>(reservation);
                    List<Customer> customers = _customerRepository.GetCustomersByDocument(GuestDocumentNumber);
                    reservationDTO.Guests = mapper.Map<List<CustomerDTO>>(customers);
                    reservationDTOList.Add(reservationDTO);
                }

                response.Result = reservationDTOList.ToList();

            }
            catch (Exception ex)
            {
                response.ResponseMessage("Error en el sistema", false, ex.Message);

            }
            return response;
        }

        #endregion

        #region PrivateMethods

        private List<string> ValidateHotelDTO(HotelDTO hotel)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(hotel.CityName))
            {
                errors.Add("Campo CityName: El nombre de la ciudad es requerido.");
            }

            if (string.IsNullOrWhiteSpace(hotel.HotelName))
            {
                errors.Add("Campo HotelName: el nombre del hotel es requerido.");
            }

            if (hotel.Rooms != null)
                ValidateRoomDTO(hotel.Rooms);

            return errors;
        }

        private List<string> ValidateRoomDTO(List<RoomDTO> rooms)
        {
            var errors = new List<string>();
            
            for (int i = 0; i < rooms.Count; i++)
            {
                var room = rooms[i];
                if (string.IsNullOrWhiteSpace(room.RoomDescription))
                {
                    errors.Add($"Room {i + 1} Campo RoomDescription: La descripcion del tipo de habitación es requerido.");
                }

                if (string.IsNullOrWhiteSpace(room.RoomName))
                {
                    errors.Add($"Room {i + 1} Campo RoomName: La descripcion del tipo de habitación es requerido.");
                }

                if (room.Price <= 0)
                {
                    errors.Add($"Room {i + 1} Campo Price: El Precio de la habitación debe ser mayor a cero.");
                }

                if (room.Taxes < 0)
                {
                    errors.Add($"Room {i + 1}: Campo Taxes: los impuestos no pueden ser negativos.");
                }
            }

            return errors;
        }

        private List<string> ValidateUpdateHotelDTO(UpdateHotelDTO hotel)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(hotel.CityName))
            {
                errors.Add("Campo CityName: El nombre de la ciudad es requerido.");
            }

            if (string.IsNullOrWhiteSpace(hotel.HotelName))
            {
                errors.Add("Campo HotelName: el nombre del hotel es requerido.");
            }

            return errors;
        }

        private List<string> ValidateUpdateRoomDTO(RoomDTO room)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(room.RoomDescription))
            {
                errors.Add($"Campo RoomDescription: La descripcion del tipo de habitación es requerido.");
            }

            if (string.IsNullOrWhiteSpace(room.RoomName))
            {
                errors.Add($"Campo RoomName: La descripcion del tipo de habitación es requerido.");
            }

            if (room.Price <= 0)
            {
                errors.Add($"Campo Price: El Precio de la habitación debe ser mayor a cero.");
            }

            if (room.Taxes < 0)
            {
               errors.Add($"Campo Taxes: los impuestos no pueden ser negativos.");
            }

            return errors;
        }

        #endregion
    }


}
