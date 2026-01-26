using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.ExportInvoice;
using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Models.Styles;
using CommercialManagement.Models.Users;
using CommercialManagement.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Models.ApplicationDBContext
{
    public class CommercialDBContext : DbContext
    {
        public CommercialDBContext(DbContextOptions<CommercialDBContext> options) : base(options)
        {
        }
        public DbSet<ApplicantConsignees> ApplicantConsignees { get; set;}
        public DbSet<Beneficiary> Beneficiary { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Fabrics> Fabrics { get; set; }
        public DbSet<GoDown> GoDown { get; set; }
        public DbSet<NotifyingParty> NotifyingParty { get; set; }
        public DbSet<Party> Party { get; set; }
        public DbSet<ExportData> ExportData { get; set; }
        public DbSet<ExportInvoices> ExportInvoices { get; set; }
        public DbSet<ExportMain> ExportMain { get; set; }
        public DbSet<ExportMainViewModel> ExportMainViewModel { get; set; }
        public DbSet<ExportLCItems> ExportLCItems { get; set; }
        public DbSet<ExportLCViewModel> ExportLCViewModel { get; set; }
        public DbSet<StyleInfo> StyleInfo { get; set; }
        public DbSet<StyleTrans> StyleTrans { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CutData>().HasNoKey();
            modelBuilder.Entity<ExportMainViewModel>().HasNoKey();
            modelBuilder.Entity<ExportLCViewModel>().HasNoKey();
        }         
    }
}
