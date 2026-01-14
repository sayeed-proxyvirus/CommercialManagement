using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialManagement.Models.Sections
{
    [Table("ItemSubSections")]
    public class ItemSubSections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SubSectionsID")]
        public int SubSectionsID { get; set; }
        [Column("SubSectionsName")]
        public string? SubSectionsName { get; set; }
        [Column("SectionsID")]
        public int SectionsID { get; set; }

    }
}
