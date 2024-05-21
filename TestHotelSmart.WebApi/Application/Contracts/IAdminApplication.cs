using TestHotelSmart.WebApi.DTO.Response;
using TestHotelSmart.WebApi.DTO.Hotel;
using TestHotelSmart.WebApi.DTO.Room;
using TestHotelSmart.WebApi.DTO.Reservation;

namespace TestHotelSmart.WebApi.Application.Contracts
{
    public interface IAdminApplication
    {
        ResponseQuery<List<HotelDTO>> GetHotels();

        ResponseQuery<List<HotelDTO>> GetHotelsActive();
        ResponseQuery<bool> CreateHotel(HotelDTO request);
        ResponseQuery<bool> CreateRooms(CreateRoomDTO request);

        ResponseQuery<bool> UpdateHotel(UpdateHotelDTO request);

        ResponseQuery<bool> UpdateRoom(RoomDTO request);

        ResponseQuery<List<ReservationDTO>> GetReservations();
    }
}
