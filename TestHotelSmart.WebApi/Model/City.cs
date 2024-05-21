using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class City
{
    public int Id { get; set; }

    public string CityName { get; set; } = null!;

    public string Section { get; set; } = null!;

    public virtual ICollection<Hotel> Hotel { get; set; } = new List<Hotel>();
}
