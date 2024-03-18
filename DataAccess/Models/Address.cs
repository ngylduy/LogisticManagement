using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class Address
    {
        public Address()
        {
            Parcels = new HashSet<Parcel>();
            Shippers = new HashSet<Shipper>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Postalcode { get; set; }

        public virtual ICollection<Parcel> Parcels { get; set; }
        public virtual ICollection<Shipper> Shippers { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
