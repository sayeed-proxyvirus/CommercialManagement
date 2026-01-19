using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Initial_Stage
{
    [Table("Fabrics")]
    public class Fabrics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ItemID")]
        public int ItemID { get; set; }
        [Column("ItemCode")]
        public string? ItemCode { get; set; }
        [Column("ItemName")]
        public string? ItemName { get; set; }
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
