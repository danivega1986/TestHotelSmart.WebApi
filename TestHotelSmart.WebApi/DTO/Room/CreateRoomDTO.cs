namespace TestHotelSmart.WebApi.DTO.Room
{
    public class CreateRoomDTO
    {
        /// <summary>
        /// Ciudad donde se encuentra el hotel
        /// </summary>
        public required string CityName { get; set; }

        /// <summary>
        /// Nombre del Holel al que se le van a crear las habitaciones
        /// </summary>
        public required string HotelName { get; set; }

        /// <summary>
        /// Detalle de las habitaciones a ser creadas
        /// </summary>
        public List<RoomDTO>? Rooms { get; set; }

    }
}
