using System.ComponentModel.DataAnnotations;
using TestHotelSmart.WebApi.DTO.Customer;

namespace TestHotelSmart.WebApi.DTO.Reservation
{
    public class CreateReservationDTO
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public decimal Taxes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public required string DocumentNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public required string FullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public required string GenderDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public required string DocumentDescription { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public required string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public required string EmergencyContactFullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public required string EmergencyContactPhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public List<CustomerDTO>? Guests { get; set; }

    }
}
