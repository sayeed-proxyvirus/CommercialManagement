using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ViewModels
{
    public class StyleTransViewModel
    {
        public int? ContactID { get; set; }
        public string? ContactNo { get; set; }
        public int? ItemID { get; set; }
        public string? ItemName { get; set; }
        public decimal? Price { get; set; }
        public decimal? FabConsumpPerDzn { get; set; }
        public decimal? ActualQuantity { get; set; }
        public decimal? ActualPrice { get; set; }
        public decimal? Booked { get; set; }
        public decimal? FabricWidth { get; set; }
    }
}
