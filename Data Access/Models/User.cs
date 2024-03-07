using System;
using System.Collections.Generic;

namespace Data_Access.Models
{
    public partial class User
    {
        public User()
        {
            Parcels = new HashSet<Parcel>();
            Shippers = new HashSet<Shipper>();
        }

        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Fullname { get; set; }
        public int? Role { get; set; }
        public int? Address { get; set; }
        public bool? Statuts { get; set; }

        public virtual Address? AddressNavigation { get; set; }
        public virtual Role? RoleNavigation { get; set; }
        public virtual ICollection<Parcel> Parcels { get; set; }
        public virtual ICollection<Shipper> Shippers { get; set; }
    }
}
