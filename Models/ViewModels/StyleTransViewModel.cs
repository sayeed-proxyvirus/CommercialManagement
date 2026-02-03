using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ViewModels
{
    public class StyleTransViewModel
    {
        public int? ContactID { get; set; }
        public string? ContactNo { get; set; }
        public int? ItemID { get; set; }
        public string? ItemName { get; set; }
        public float? Price { get; set; }
        public float? FabConsumpPerDzn { get; set; }
        public float? ActualQuantity { get; set; }
        public float? ActualPrice { get; set; }
        public float? Booked { get; set; }
        public float? FabricWidth { get; set; }
    }
}
