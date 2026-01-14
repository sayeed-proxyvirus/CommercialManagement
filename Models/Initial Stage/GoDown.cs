using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Initial_Stage
{
    [Table("GoDown")]
    public class GoDown
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("GDID")]
        public int GDID { get; set; }
        [Column("GDName")]
        public string? GDName { get; set; }
        [Column("GDPhone")]
        public string? GDPhone { get; set; }
        [Column("GDAddress")]
        public string? GDAddress { get; set; }
    }
}
