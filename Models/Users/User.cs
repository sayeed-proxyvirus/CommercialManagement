using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommercialManagement.Models.Users
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserID")]
        public int UserID { get; set; }
        [Column("UserName")]
        public string? UserName { get; set; }
        [Column("Upassword")]
        public string? Upassword { get; set; }
        [Column("FullName")]
        public string? FullName { get; set; }
        [Column("Email")]
        public string? Email { get; set; }
    }
}
