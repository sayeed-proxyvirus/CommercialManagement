using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.ExportInvoice;
using CommercialManagement.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.Exports.ExportsServiceImple
{
    public class ExportInvoiceServiceImple : ExportInvoiceService
    {
        private readonly CommercialDBContext _context;
        public ExportInvoiceServiceImple(CommercialDBContext context)
        {
            _context = context;
        }

        public bool AddExportInvoices(ExportInvoices exportInvoices)
        {
            try
            {
                _context.ExportInvoices.Add(exportInvoices);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ExportInvoices GetbyId(int Id)
        {
            try
            {
                var exportInv = _context.ExportInvoices
                    .FirstOrDefault(i => i.ExpInvID == Id);
                return exportInv;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ExportInvoiceViewModel> GetExportInvoices(string ExpInv)
        {
            try
            {
                var param = new SqlParameter("@ExpInv", ExpInv);
                var exportInvVM = _context.ExportInvoiceViewModel
                    .FromSqlRaw("EXEC usp_FindExpInvData @ExpInv", param)
                    .ToList();
                return exportInvVM;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateExportInvoices(ExportInvoices exportInvoices)
        {
            try
            {
                _context.ExportInvoices.Update(exportInvoices);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
