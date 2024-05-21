using Microsoft.EntityFrameworkCore;
using TestHotelSmart.WebApi.DataAccess.Repositories.Contractos;
using TestHotelSmart.WebApi.Helpers;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DataAccess.Repositories
{
    class RoomRepository : IRoomRepository
    {
        #region Fields
        protected readonly TestHotelContext _context;
        #endregion

        #region Builders

        public RoomRepository(TestHotelContext context)
        {
            _context = context;
        }
        #endregion


        #region Methods

        public IEnumerable<Room> List()
        {
            return _context.Set<Room>().AsNoTracking().Include(x => x.IdRoomTypeNavigation).ToList();
        }

        public void Insert(Room room)
        {
            _context.Room.Add(room);
            _context.SaveChanges();
        }

        public void InsertList(List<Room> rooms)
        {
            _context.Room.AddRange(rooms);
            _context.SaveChanges();
        }

        public void Update(Room room)
        {
            var originalRoom = _context.Room.FirstOrDefault(x => x.Id == room.Id);
            FrammeworkTypeUtility.SetProperties(room, originalRoom);
            _context.SaveChanges();
        }

        public Room? GetRoom(int id)
        {
            return _context.Room.FirstOrDefault(x => x.Id == id);
        }
        #endregion

    }
}
