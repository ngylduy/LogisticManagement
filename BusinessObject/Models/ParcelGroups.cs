namespace BusinessObject.Models
{
    public class ParcelGroups
    {
        public string? Id { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }

        public ICollection<ParcelGroupItems>? ParcelGroupItems { get; set; }
        public ICollection<DeliveryRoutes>? DeliveryRoutes { get; set; }
    }
}
