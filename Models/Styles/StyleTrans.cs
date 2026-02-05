using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Styles
{
    [Table("StyleTrans")]
    public class StyleTrans
    {
        [Key]
        [Column("ContactID")]
        public int? ContactID { get; set; }
        [Column("ContactNo")]
        public string? ContactNo { get; set; }
        [Column("ItemID")]
        public int? ItemID { get; set; }
        [Column("ItemName")]
        public string? ItemName { get; set; }
        [Column("Price")]
        public decimal? Price { get; set; }
        [Column("FabConsumpPerDzn")]
        public decimal? FabConsumpPerDzn { get; set; }
        [Column("ActualQuantity")]
        public decimal? ActualQuantity { get; set; }
        [Column("ActualPrice")]
        public decimal? ActualPrice { get; set; }
        [Column("Booked")]
        public decimal? Booked { get; set; }
        [Column("FabricWidth")]
        public decimal? FabricWidth { get; set; }
    }
}
