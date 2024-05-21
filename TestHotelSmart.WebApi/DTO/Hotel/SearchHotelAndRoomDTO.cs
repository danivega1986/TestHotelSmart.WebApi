using System.ComponentModel.DataAnnotations;

namespace TestHotelSmart.WebApi.DTO.Hotel
{
    public class SearchHotelAndRoomDTO
    {

        /// <summary>
        /// Fecha de ingreso
        /// </summary>
        [Required]
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Fecha de Salida
        /// </summary>
        [Required]
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Nombre de la Ciudad donde se encuentra el Hotel
        /// </summary>
        public required string CityName { get; set; }

        /// <summary>
        /// Nombre del Hotel
        /// </summary>
        [Required]
        public int Quantity { get; set; }

    }
}
