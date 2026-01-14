using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Sections
{
    [Table("ItemSections")]
    public class ItemSections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SectionsID")]
        public int SectionsID { get; set; }
        [Column("SectionsName")]
        public string? SectionsName { get; set; }
    }
}
