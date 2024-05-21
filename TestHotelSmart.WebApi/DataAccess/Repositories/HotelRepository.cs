using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TestHotelSmart.WebApi.DataAccess.Repositories.Contractos;
using TestHotelSmart.WebApi.Helpers;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories
{
    class HotelRepository : IHotelRepository
    {
        #region Fields
        protected readonly TestHotelContext _context;
        #endregion

        #region Builders

        public HotelRepository(TestHotelContext context)
        {
            _context = context;
        }
        #endregion


        #region Methods

        public IEnumerable<Hotel> List()
        {
            return _context.Set<Hotel>().AsNoTracking().Include(x => x.IdCityNavigation)
                .Include(x => x.Room).ThenInclude(x => x.IdRoomTypeNavigation)
                .OrderBy(x => x.IdCityNavigation.CityName).ToList();
        }

        public void Insert(Hotel hotel)
        {
            _context.Hotel.Add(hotel);
            _context.SaveChanges();
        }

        public void Update(Hotel hotel)
        {
            var originalHotel = _context.Hotel.FirstOrDefault(x => x.Id == hotel.Id);
            FrammeworkTypeUtility.SetProperties(hotel, originalHotel);
            _context.SaveChanges();
        }

        public Hotel? GetHotel(int id)
        {
            return _context.Hotel.FirstOrDefault(x => x.Id == id);
        }

        public Hotel? GetHotelByCityAndName(string hotelName, int id)
        {
            return _context.Hotel.FirstOrDefault(x => x.HotelName.Equals(hotelName) && x.IdCity == id); ;
        }

        #endregion

    }
}
