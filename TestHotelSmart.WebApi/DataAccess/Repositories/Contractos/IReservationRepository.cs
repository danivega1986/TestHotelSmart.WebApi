using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories.Contractos
{
    internal interface IReservationRepository
    {
        IEnumerable<Reservation> List();
        void Insert(Reservation reservation);
        void InsertTransact(Reservation reservation);
    }
}

