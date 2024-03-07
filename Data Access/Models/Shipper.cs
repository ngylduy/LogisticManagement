using System;
using System.Collections.Generic;

namespace Data_Access.Models
{
    public partial class Shipper
    {
        public int Id { get; set; }
        public int? Userid { get; set; }
        public int? Area { get; set; }
        public bool? Status { get; set; }

        public virtual Address? AreaNavigation { get; set; }
        public virtual User? User { get; set; }
    }
}
