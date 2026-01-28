using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ExportInvoice
{
    [Table("ExportInvoice")]
    public class ExportInvoices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ExpInvID")]
        public int ExpInvID { get; set; }
        [Column("ExpInv")]
        public string? ExpInv { get; set; }
        [Column("ExportDate")]
        public DateTime? ExportDate { get; set; }
        [Column("FDBPNo")]
        public string? FDBPNo { get; set; }
        [Column("FDBPDate")]
        public DateTime? FDBPDate { get; set; }
        [Column("ShipBill")]
        public string? ShipBill { get; set; }
        [Column("ShipBillDate")]
        public DateTime? ShipBillDate { get; set; }
        [Column("BillNo")]
        public string? BillNo { get; set; }
        [Column("BillDate")]
        public DateTime? BillDate { get; set; }
        [Column("ExpNo")]
        public string? ExpNo { get; set; }
        [Column("ExpDate")]
        public DateTime? ExpDate { get; set; }
        [Column("ExpLCNo")]
        public string? ExpLCNo { get; set; }
    }
}
