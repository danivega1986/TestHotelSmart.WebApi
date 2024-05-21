using Microsoft.AspNetCore.Mvc;
using TestHotelSmart.WebApi.Application.Contracts;
using TestHotelSmart.WebApi.DTO.Hotel;
using TestHotelSmart.WebApi.DTO.Reservation;
using TestHotelSmart.WebApi.DTO.Response;
using TestHotelSmart.WebApi.DTO.Room;

namespace TestHotelSmart.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        #region Fields
        private readonly IClientApplication _clientApplication;

        #endregion

        #region Builder

        public ClientController(IClientApplication clientApplication)
        {
            _clientApplication = clientApplication;
        }


        #endregion

        #region Methods

        [HttpPost]
        [Route(nameof(ClientController.GetRoomsAvailable))]
        public async Task<ResponseQuery<List<HotelDTO>>> GetRoomsAvailable(SearchHotelAndRoomDTO search)
        {
            return await Task.Run(() =>
            {
                return _clientApplication.GetRoomsAvailable(search);
            });
        }


        [HttpPost]
        [Route(nameof(ClientController.CreateReservation))]
        public async Task<ResponseQuery<bool>> CreateReservation(CreateReservationDTO request)
        {
            return await Task.Run(() =>
            {
                return _clientApplication.CreateReservation(request);
            });
        }
        #endregion
    }
}
