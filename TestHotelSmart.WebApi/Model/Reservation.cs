using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class Reservation
{
    public int Id { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public int IdRoom { get; set; }

    public DateTime ArrivalDate { get; set; }

    public DateTime DepartureDate { get; set; }

    public int Quantity { get; set; }

    public string EmergencyContactFullName { get; set; } = null!;

    public string EmergencyContactPhoneNumber { get; set; } = null!;

    public virtual Customer DocumentNumberNavigation { get; set; } = null!;

    public virtual ICollection<Guest> Guest { get; set; } = new List<Guest>();

    public virtual Room IdRoomNavigation { get; set; } = null!;
}
