using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories.Contractos
{
    internal interface IRoomRepository
    {
        IEnumerable<Room> List();
        void Insert(Room room);
        void InsertList(List<Room> rooms);
        void Update(Room room);
        Room? GetRoom(int id);
    }
}
