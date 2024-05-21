using TestHotelSmart.WebApi.DTO.Hotel;
using TestHotelSmart.WebApi.DTO.Reservation;
using TestHotelSmart.WebApi.DTO.Response;

namespace TestHotelSmart.WebApi.Application.Contracts
{
    public interface IClientApplication
    {
        ResponseQuery<List<HotelDTO>> GetRoomsAvailable(SearchHotelAndRoomDTO search);

        ResponseQuery<bool> CreateReservation(CreateReservationDTO request);
    }
}
