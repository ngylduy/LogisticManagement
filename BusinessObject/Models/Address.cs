namespace BusinessObject.Models
{
    public partial class Address
    {
        public string? Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Postalcode { get; set; }

        public ICollection<AppUser>? appUsers { get; set; }

        public ICollection<Parcel>? deliveryParcel { get; set; }

        public ICollection<Parcel>? pickupParcel { get; set; }


    }
}
