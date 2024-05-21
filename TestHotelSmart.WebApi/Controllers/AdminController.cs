using TestHotelSmart.WebApi.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using TestHotelSmart.WebApi.Application.Contracts;
using TestHotelSmart.WebApi.DTO.Hotel;
using TestHotelSmart.WebApi.DTO.Room;
using TestHotelSmart.WebApi.DTO.Reservation;

namespace TestHotelSmart.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        #region Fields
        private readonly IAdminApplication _adminApplication;

        #endregion

        #region Builder

        public AdminController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }


        #endregion

        #region Methods

        [HttpGet]
        [Route(nameof(AdminController.GetHotels))]
        public async Task<ResponseQuery<List<HotelDTO>>> GetHotels()
        {
            return await Task.Run(() =>
            {
                return _adminApplication.GetHotels();
            });
        }

        [HttpGet]
        [Route(nameof(AdminController.GetHotelsActive))]
        public async Task<ResponseQuery<List<HotelDTO>>> GetHotelsActive()
        {
            return await Task.Run(() =>
            {
                return _adminApplication.GetHotelsActive();
            });
        }

        [HttpGet]
        [Route(nameof(AdminController.GetReservations))]
        public async Task<ResponseQuery<List<ReservationDTO>>> GetReservations()
        {
            return await Task.Run(() =>
            {
                return _adminApplication.GetReservations();
            });
        }

        [HttpPost]
        [Route(nameof(AdminController.CreateHotel))]
        public async Task<ResponseQuery<bool>> CreateHotel(HotelDTO request)
        {
            return await Task.Run(() =>
            {
                return _adminApplication.CreateHotel(request);
            });
        }

        [HttpPost]
        [Route(nameof(AdminController.CreateRooms))]
        public async Task<ResponseQuery<bool>> CreateRooms(CreateRoomDTO request)
        {
            return await Task.Run(() =>
            {
                return _adminApplication.CreateRooms(request);
            });
        }

        [HttpPatch]
        [Route(nameof(AdminController.UpdateHotel))]
        public async Task<ResponseQuery<bool>> UpdateHotel(UpdateHotelDTO request)
        {
            return await Task.Run(() =>
            {
                return _adminApplication.UpdateHotel(request);
            });
        }

        [HttpPatch]
        [Route(nameof(AdminController.UpdateRoom))]
        public async Task<ResponseQuery<bool>> UpdateRoom(RoomDTO request)
        {
            return await Task.Run(() =>
            {
                return _adminApplication.UpdateRoom(request);
            });
        }

        #endregion
    }
}
