using Microsoft.AspNetCore.Identity;

namespace BusinessObject.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? HouseNumber { get; set; }
        public string? AddressId { get; set; }
        public Address? Address { get; set; }

        public ICollection<Parcel>? SenderParcels { get; set; }
        public ICollection<Parcel>? ReceiverParcels { get; set; }
    }
}
