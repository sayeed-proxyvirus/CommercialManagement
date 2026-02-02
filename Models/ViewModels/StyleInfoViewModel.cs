using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ViewModels
{
    public class StyleInfoViewModel
    {
        public int? ContactID { get; set; }
        public string? ContactNo { get; set; }
        public string? StyleDesc { get; set; }
        public string? StyleName { get; set; }
        public int? CustID { get; set; }
        public string? CustName { get; set; }
        public string? CustCode { get; set; }
        public int? BenID { get; set; }
        public string? BenName { get; set; }
        public string? BenCode { get; set; }
        public string? Wash { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? TotalFabricQuantity { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public decimal? StyleCM { get; set; }
        public decimal? Lbi { get; set; }
        public decimal? WashingCharge { get; set; } 
        public decimal? FabricDozens { get; set; }
        public decimal? FabricsPerPcs { get; set; }
        public string? OrderNo { get; set; }
        public decimal? LACPerPcs { get; set; }
    }
}
