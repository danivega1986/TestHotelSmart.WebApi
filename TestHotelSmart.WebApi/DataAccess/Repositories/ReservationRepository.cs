using Microsoft.EntityFrameworkCore;
using TestHotelSmart.WebApi.DataAccess.Repositories.Contractos;
using TestHotelSmart.WebApi.Helpers;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories
{
    public class ReservationRepository : IReservationRepository
    {

        #region Fields
        protected readonly TestHotelContext _context;
        #endregion

        #region Builders

        public ReservationRepository(TestHotelContext context)
        {
            _context = context;
        }
        #endregion


        #region Methods

        public IEnumerable<Reservation> List()
        {
            return _context.Set<Reservation>().AsNoTracking().ToList();
        }

        public void Insert(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            _context.SaveChanges();
        }

        public void InsertTransact(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
        }

        #endregion

    }
}
