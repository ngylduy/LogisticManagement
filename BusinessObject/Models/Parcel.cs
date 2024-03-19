namespace BusinessObject.Models
{
    public partial class Parcel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int? Weight { get; set; }
        public string? Dimensions { get; set; }

        public string? SenderUserId { get; set; }
        public string? ReceiverUserId { get; set; }

        public string? PickupAddressId { get; set; }
        public string? DeliveryAddressId { get; set; }

        public enum Status { Pending, InTransit, Delivered };

        public Address? PickupAddress { get; set; }
        public Address? DeliveryAddress { get; set; }
        public AppUser? SenderUser { get; set; }
        public AppUser? ReceiverUser { get; set; }

        public ICollection<ParcelGroupItems>? ParcelGroupItems { get; set; }
    }
}
