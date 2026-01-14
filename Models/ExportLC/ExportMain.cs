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
        public string? MainExpName { get; set; }
        [Column("ReceiveDate")]
        public DateTime? ReceiveDate { get; set; }
        [Column("ExpDate")]
        public DateTime? ExpDate { get; set; }
        [Column("Dollars")]
        public float? Dollars { get; set; }
        [Column("MainExpTaka")]
        public float? MainExpTaka { get; set; }
        [Column("ExpQuantity")]
        public float? ExpQuantity { get; set; }
        [Column("ApplicantID")]
        public int? ApplicantID { get; set; }
        [Column("ApplicantCode")]
        public string? ApplicantCode { get; set; }
        [Column("BenID")]
        public int? BenID { get; set; }
        [Column("BenCode")]
        public string? BenCode { get; set; }
        [Column("PartyID")]
        public int PartyID { get; set; }
        [Column("PartyCode")]
        public string? PartyCode { get; set; }
    }
}
