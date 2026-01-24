using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Initial_Stage
{
    [Table("Party")]
    public class Party
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
    }
}
