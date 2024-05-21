
using TestHotelSmart.WebApi.DTO.Response;
using TestHotelSmart.WebApi.DTO.Room;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories.Contractos
{
    internal interface IHotelRepository
    {
        IEnumerable<Hotel> List();
        void Insert(Hotel hotel);
        void Update(Hotel hotel);
        Hotel? GetHotel(int id);
        Hotel? GetHotelByCityAndName(string hotelName, int id);
    }
}