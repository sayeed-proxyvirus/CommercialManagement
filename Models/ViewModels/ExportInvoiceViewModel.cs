using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ViewModels
{
    public class ExportInvoiceViewModel
    {
        public int ExpInvID { get; set; }
        public string? ExpInv { get; set; }
        public DateTime? ExportDate { get; set; }
        public string? FDBPNo { get; set; }
        public DateTime? FDBPDate { get; set; }
        public string? ShipBill { get; set; }
        public DateTime? ShipBillDate { get; set; }
        public string? BillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string? ExpNo { get; set; }
        public DateTime? ExpDate { get; set; }
        public string? ExpLCNo { get; set; }
        public string? MainExpRem { get; set; }
    }
}
