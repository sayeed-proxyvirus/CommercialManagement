using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models
{
    [Table("CutData")]
    public class CutData
    {
        [Column("ExpID")]
        public int ExpID { get; set; }
        [Column("ExpLCNo")]
        public string? ExpLCNo { get; set; }
        [Column("ContactID")]
        public string? ContactID { get; set; }
        [Column("ContactNo")]
        public string? ContactNo { get; set; }
        [Column("CuttingQuantity")]
        public float? CuttingQuantity { get; set; }
        [Column("CuttignConDzn")]
        public float? CuttignConDzn { get; set; }
        [Column("ItemID")]
        public int? ItemID { get; set; }
        [Column("ItemCode")]
        public string? ItemCode { get; set; }
    }
}
