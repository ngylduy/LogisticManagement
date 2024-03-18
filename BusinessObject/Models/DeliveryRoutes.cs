namespace BusinessObject.Models
{
    public class DeliveryRoutes
    {
        public string Id { get; set; }
        public string? GroupId { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }

        public enum Status { Scheduled, Active, Completed };

        public ParcelGroups? Group { get; set; }
    }
}
