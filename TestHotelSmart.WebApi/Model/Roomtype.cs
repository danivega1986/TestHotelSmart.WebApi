using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class Roomtype
{
    public int Id { get; set; }

    public string RoomDescription { get; set; } = null!;

    public virtual ICollection<Room> Room { get; set; } = new List<Room>();
}
