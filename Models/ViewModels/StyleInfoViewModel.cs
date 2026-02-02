using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ViewModels
{
    public class StyleInfoViewModel
    {
        public int? ContactID { get; set; }
        public string? ContactNo { get; set; }
        public string? StyleDesc { get; set; }
        public string? StyleName { get; set; }
        public int? CustName { get; set; }
        public string? CustCode { get; set; }
        public int? BenName { get; set; }
        public string? BenCode { get; set; }
        public string? Wash { get; set; }
        public float? TotalQuantity { get; set; }
        public float? TotalFabricsQuantity { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public float? StyleCM { get; set; }
        public float? Lbi { get; set; }
        public float? WashingCharge { get; set; } 
        public float? FabricDozens { get; set; }
        public float? FabricsPerPcs { get; set; }
        public string? OrderNo { get; set; }
        public float? LACPerPcs { get; set; }
    }
}
