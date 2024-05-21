using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class Customer
{
    public string DocumentNumber { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int IdGender { get; set; }

    public int IdDocumentType { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Guest> Guest { get; set; } = new List<Guest>();

    public virtual Documenttype IdDocumentTypeNavigation { get; set; } = null!;

    public virtual Gender IdGenderNavigation { get; set; } = null!;

    public virtual ICollection<Reservation> Reservation { get; set; } = new List<Reservation>();
}
