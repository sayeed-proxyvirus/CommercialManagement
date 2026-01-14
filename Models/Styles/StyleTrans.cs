using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Styles
{
    [Table("StyleTrans")]
    public class StyleTrans
    {
        [Key]
        [Column("ContactID")]
        public string? ContactID { get; set; }
        [Column("ContactNo")]
        public string? ContactNo { get; set; }
        [Column("ItemID")]
        public int? ItemID { get; set; }
        [Column("ItemCode")]
        public string? ItemCode { get; set; }
        [Column("Price")]
        public float? Price { get; set; }
        [Column("FabConsumpPerDzn")]
        public float? FabConsumpPerDzn { get; set; }
        [Column("ActualQuantity")]
        public float? ActualQuantity { get; set; }
        [Column("ActualPrice")]
        public float? ActualPrice { get; set; }
        [Column("Booked")]
        public float? Booked { get; set; }
        [Column("FabricWidth")]
        public float? FabricWidth { get; set; }
    }
}
