using System;
using System.Collections.Generic;

namespace TestHotelSmart.WebApi.Model;

public partial class Documenttype
{
    public int Id { get; set; }

    public string DocumentDescription { get; set; } = null!;

    public virtual ICollection<Customer> Customer { get; set; } = new List<Customer>();
}
