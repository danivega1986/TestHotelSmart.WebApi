using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories.Contractos
{
    internal interface IGuestRepository
    {
        List<string> GetGuestsByReservation(int idReservation);

        void InsertTransactList(List<Guest> guests);
    }
}
