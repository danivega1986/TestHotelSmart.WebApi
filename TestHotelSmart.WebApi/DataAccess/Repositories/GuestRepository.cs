using Microsoft.EntityFrameworkCore;
using TestHotelSmart.WebApi.DataAccess.Repositories.Contractos;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        #region Fields
        protected readonly TestHotelContext _context;
        #endregion

        #region Builders

        public GuestRepository(TestHotelContext context)
        {
            _context = context;
        }
        #endregion
        public List<string> GetGuestsByReservation(int idReservation)
        {
            return _context.Guest.Where(x => x.IdReservation == idReservation).Select(x => x.DocumentNumber).ToList();
        }

        public void InsertTransactList(List<Guest> guests)
        {
            _context.Guest.AddRange(guests);
        }
    }
}
