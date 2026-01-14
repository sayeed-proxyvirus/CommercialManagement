using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Initial_Stage
{
    [Table("NotifyingParty")]
    public class NotifyingParty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PartyID")]
        public int PartyID { get; set; }
        [Column("PartyCode")]
        public string? PartyCode { get; set; }
        [Column("PartyName")]
        public string? PartyName { get; set; }
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
