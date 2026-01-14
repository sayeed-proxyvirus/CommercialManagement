using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Initial_Stage
{
    [Table("ApplicantConsignees")]
    public class ApplicantConsignees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ApplicantID")]
        public int ApplicantID { get; set; }
        [Column("ApplicantCode")]
        public string? ApplicantCode { get; set; }
        [Column("ApplicantName")]
        public string? ApplicantName { get; set; }
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
