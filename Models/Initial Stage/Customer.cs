using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Initial_Stage
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CustID")]
        public int? CustID { get; set; }
        [Column("CustCode")]
        public string? CustCode { get; set; }
        [Column("CustName")]
        public string? CustName { get; set; }
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
