using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Styles
{
    [Table("StyleInfo")]
    public class StyleInfo
    {
        [Key]
        [Column("ContactID")]
        public int? ContactID { get; set; }
        [Column("ContactNo")]
        public string? ContactNo { get; set; }
        [Column("StyleDesc")]
        public string? StyleDesc { get; set; }
        [Column("StyleName")]
        public string? StyleName { get; set; }
        [Column("CustID")]
        public int? CustID { get; set; }
        [Column("CustCode")]
        public string? CustCode { get; set; }
        [Column("BenID")]
        public int? BenID { get; set; }
        [Column("BenCode")]
        public string? BenCode { get; set; }
        [Column("Wash")]
        public string? Wash { get; set; }
        [Column("TotalQuantity")]
        public decimal? TotalQuantity { get; set; }
        [Column("TotalFabricQuantity")]
        public decimal? TotalFabricQuantity { get; set; }
        [Column("ShipmentDate")]
        public DateTime? ShipmentDate { get; set; }
        [Column("StyleCM")]
        public decimal? StyleCM { get; set; }
        [Column("Lbi")]
        public decimal? Lbi { get; set; }
        [Column("WashingCharge")]
        public decimal? WashingCharge { get; set; }
        [Column("FabricDozens")]
        public decimal? FabricDozens { get; set; }
        [Column("FabricsPerPcs")]
        public decimal? FabricsPerPcs { get; set; }
        [Column("OrderNo")]
        public string? OrderNo { get; set; }
        [Column("LACPerPcs")]
        public decimal? LACPerPcs { get; set; }

    }
}
