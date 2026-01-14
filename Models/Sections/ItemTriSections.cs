using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CommercialManagement.Models.Sections
{
    [Table("ItemTriSections")]
    public class ItemTriSections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("TriSectionsID")]
        public int TriSectionsID { get; set; }
        [Column("TriSectionsName")]
        public string? TriSectionsName { get; set; }
        [Column("SectionsID")]
        public int SectionsID { get; set; }
        [Column("SubSectionsID")]
        public int SubSectionsID { get; set; }

    }
}
