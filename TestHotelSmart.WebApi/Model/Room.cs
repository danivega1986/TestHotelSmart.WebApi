using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class Room
{
    public int Id { get; set; }

    public int IdHotel { get; set; }

    public bool IsActive { get; set; }

    public decimal Price { get; set; }

    public decimal Taxes { get; set; }

    public int IdRoomType { get; set; }

    public string RoomName { get; set; } = null!;

    public virtual Hotel IdHotelNavigation { get; set; } = null!;

    public virtual Roomtype IdRoomTypeNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservation { get; set; } = new List<Reservation>();
}
