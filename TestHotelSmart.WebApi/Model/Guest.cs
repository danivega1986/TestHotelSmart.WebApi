using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class Guest
{
    public int Id { get; set; }

    public string DocumentNumber { get; set; } = null!;

    public int IdReservation { get; set; }

    public virtual Customer DocumentNumberNavigation { get; set; } = null!;

    public virtual Reservation IdReservationNavigation { get; set; } = null!;
}
