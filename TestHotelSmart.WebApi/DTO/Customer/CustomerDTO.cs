using System.ComponentModel.DataAnnotations;

namespace TestHotelSmart.WebApi.DTO.Customer
{
    public class CustomerDTO
    {
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

    }
}
