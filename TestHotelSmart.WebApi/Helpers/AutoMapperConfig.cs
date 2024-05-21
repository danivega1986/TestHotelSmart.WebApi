using AutoMapper;
using TestHotelSmart.WebApi.DTO.Customer;
using TestHotelSmart.WebApi.DTO.Hotel;
using TestHotelSmart.WebApi.DTO.Reservation;
using TestHotelSmart.WebApi.DTO.Room;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.Helpers
{
    public class AutomapperConfig : Profile
    {

        public AutomapperConfig()
        {

            // Configuración del mapeo para Room a RoomDTO
            CreateMap<Room, RoomDTO>()
                .ForMember(dest => dest.RoomDescription, opt => opt.MapFrom(src => src.IdRoomTypeNavigation.RoomDescription));

            // Configuración del mapeo para Room a RoomDTO
            CreateMap<RoomDTO, Room>();

            // Configuración del mapeo para Hotel a HotelDTO
            CreateMap<Hotel, HotelDTO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.IdCityNavigation.CityName))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Room));

            CreateMap<HotelDTO, Hotel>();

            // Configuración del mapeo para Reservation a ReservationDTO
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(src => src.DocumentNumber))
                .ForMember(dest => dest.IdRoom, opt => opt.MapFrom(src => src.IdRoom))
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(src => src.ArrivalDate))
                .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.DepartureDate))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.EmergencyContactFullName, opt => opt.MapFrom(src => src.EmergencyContactFullName))
                .ForMember(dest => dest.EmergencyContactPhoneNumber, opt => opt.MapFrom(src => src.EmergencyContactPhoneNumber));

            // Configuración del mapeo para CustomerDTO a Customer
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.DocumentDescription, opt => opt.MapFrom(src => src.IdDocumentTypeNavigation.DocumentDescription))
                .ForMember(dest => dest.GenderDescription, opt => opt.MapFrom(src => src.IdGenderNavigation.GenderDescription));


            CreateMap<CustomerDTO, Customer>();
        }

    }
}