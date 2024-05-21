using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DTO.Room
{
    public class RoomDTO
    {

        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Se encuentra acitvo
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Precio Base
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Impuestos
        /// </summary>
        public decimal Taxes { get; set; }

        /// <summary>
        /// Descripcion del tipo de la habitacion
        /// </summary>
        public required string RoomDescription { get; set; }

        /// <summary>
        /// Descripcion del nombre de la habitación
        /// </summary>
        public required string RoomName { get; set; }
    }
}
