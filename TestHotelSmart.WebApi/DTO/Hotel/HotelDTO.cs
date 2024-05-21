using TestHotelSmart.WebApi.DTO.Room;

namespace TestHotelSmart.WebApi.DTO.Hotel
{
    public class HotelDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Estado del Hotel Activo, Inactivo
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Nombre de la Ciudad donde se encuentra el Hotel
        /// </summary>
        public required string CityName { get; set; }

        /// <summary>
        /// Nombre del Hotel
        /// </summary>
        public required string HotelName { get; set; }

        public List<RoomDTO>? Rooms { get; set; }
    }
}
