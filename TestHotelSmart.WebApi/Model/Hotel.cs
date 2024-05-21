using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class Hotel
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public int IdCity { get; set; }

    public string HotelName { get; set; } = null!;

    public virtual City IdCityNavigation { get; set; } = null!;

    public virtual ICollection<Room> Room { get; set; } = new List<Room>();
}
