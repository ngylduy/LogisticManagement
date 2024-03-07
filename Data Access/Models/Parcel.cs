using System;
using System.Collections.Generic;

namespace Data_Access.Models
{
    public partial class Parcel
    {
        public int Id { get; set; }
        public int? Weight { get; set; }
        public string? Dimensions { get; set; }
        public int? Sellerid { get; set; }
        public string? Customername { get; set; }
        public string? Customerphone { get; set; }
        public int? Customeraddress { get; set; }
        public bool? Status { get; set; }

        public virtual Address? CustomeraddressNavigation { get; set; }
        public virtual User? Seller { get; set; }
    }
}
