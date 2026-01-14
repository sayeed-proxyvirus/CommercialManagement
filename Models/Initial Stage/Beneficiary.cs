using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Initial_Stage
{
    [Table("Beneficiary")]
    public class Beneficiary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BenID")]
        public int? BenID { get; set; }
        [Column("BenCode")]
        public string? BenCode { get; set; }
        [Column("BenName")]
        public string? BenName { get; set; }
        [Column("Remarks")]
        public string? Remarks { get; set; }
        [Column("Address")]
        public string? Address { get; set; }
        [Column("CreatedAt")]
        public DateTime? CreatedAt { get; set; }
        [Column("CreatedBy")]
        public string? CreatedBy { get; set; }
        [Column("ModifiedAt")]
        public DateTime? ModifiedAt { get; set; }
        [Column("ModifiedBy")]
        public string? ModifiedBy { get; set; }
    }
}
