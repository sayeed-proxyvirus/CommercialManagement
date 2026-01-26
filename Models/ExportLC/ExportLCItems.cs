using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.ExpLC
{
    [Table("ExportLCItems")]
    public class ExportLCItems
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ExpID")]
        public int ExpID { get; set; }
        [Column("ExpLCNo")]
        public string? ExpLCNo { get; set; }
        [Column("ContactNo")]
        public string? ContactNo { get; set; }
        [Column("TotalPcs")]
        public int? TotalPcs { get; set; }
        [Column("TotalPrice")]
        public double? TotalPrice { get; set; }

    }
}
