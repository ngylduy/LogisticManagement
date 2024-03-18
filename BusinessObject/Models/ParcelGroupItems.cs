namespace BusinessObject.Models
{
    public class ParcelGroupItems
    {
        public string? ParcelId { get; set; }
        public string? ParcelGroupId { get; set; }

        public Parcel? ParcelItem { get; set; }
        public ParcelGroups? ParcelGroup { get; set; }
    }
}
