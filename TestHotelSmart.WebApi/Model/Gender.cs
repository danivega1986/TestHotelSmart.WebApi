using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class Gender
{
    public int Id { get; set; }

    public string GenderDescription { get; set; } = null!;

    public virtual ICollection<Customer> Customer { get; set; } = new List<Customer>();
}
