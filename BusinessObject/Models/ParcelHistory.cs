namespace BusinessObject.Models
{
    public class ParcelHistory
    {
        public string Id { get; set; }
        public string ParcelId { get; set; }
        public string? Status { get; set; }
        public DateTime Date { get; set; }
        public string? AddressId { get; set; }
        public Address? Address { get; set; }
        public Parcel? Parcel { get; set; }
    }
}
