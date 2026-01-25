using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ExpLC
{
    [Table("ExportMain")]
    public class ExportMain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ExpID")]
        public int ExpID { get; set; }
        [Column("MainExpName")]
        public string MainExpName { get; set; }
        [Column("ReceiveDate")]
        public DateTime? ReceiveDate { get; set; }
        [Column("ExpDate")]
        public DateTime? ExpDate { get; set; }
        [Column("Dollars")]
        public decimal? Dollars { get; set; }
        [Column("MainExpTaka")]
        public decimal? MainExpTaka { get; set; }
        [Column("ExpQuanity")]
        public decimal? ExpQuanity { get; set; }
        [Column("ApplicantCode")]
        public string? ApplicantCode { get; set; }
        [Column("BenCode")]
        public string? BenCode { get; set; }
        [Column("PartyCode")]
        public string? PartyCode { get; set; }
        [Column("Destination")]
        public string? Destination { get; set; }
    }
}
