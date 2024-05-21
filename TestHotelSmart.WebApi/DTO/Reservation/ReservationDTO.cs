using TestHotelSmart.WebApi.DTO.Customer;
using TestHotelSmart.WebApi.Model;

namespace TestHotelSmart.WebApi.DTO.Reservation
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public required string DocumentNumber { get; set; }

        public int IdRoom { get; set; }

        public DateTime ArrivalDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public int Quantity { get; set; }

        public required string EmergencyContactFullName { get; set; }

        public required string EmergencyContactPhoneNumber { get; set; }

        public required List<CustomerDTO> Guests { get; set; }

    }
}
