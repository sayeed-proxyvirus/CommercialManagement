using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ExportInvoice
{
    [Table("ExportData")]
    public class ExportData
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ContactID")]
        public int? ContactID { get; set; }
        [Column("ContactNo")]
        public string? ContactNo { get; set; }
        //[Column("ExpInvID")]
        //public int ExpInvID { get; set; }
        [Column("ExpInv")]
        public string? ExpInv { get; set; }
        [Column("ExpQuantity")]
        public int? ExpQuantity { get; set; }
        [Column("UnitPrice")]
        public float? UnitPrice { get; set; }
    }
}
