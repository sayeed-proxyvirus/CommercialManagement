using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.ExportInvoice;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.Exports.ExportsServiceImple
{
    public class ExportLCItemsServiceImple : ExportLCItemsService
    {
        private readonly CommercialDBContext _context;
        public ExportLCItemsServiceImple(CommercialDBContext context)
        {
            _context = context;
        }

        public bool AddExportLCItems(ExportLCItems exportLCItems)
        {
            try
            {
                _context.ExportLCItems.Add(exportLCItems);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ExportLCItems GetbyId(int Id)
        {
            try
            {
                var exportLC = _context.ExportLCItems
                    .FirstOrDefault(c => c.ExpID == Id);
                return exportLC;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ExportLCItems> GetExportLCItems(string ExpLCNo)
        {
            try
            {
                var param = new SqlParameter("@ExpLCName", ExpLCNo);
                var exportLC = _context.ExportLCItems
                    .FromSqlRaw("EXEC usp_FindLCContactData @ExpLCName", param)
                    .ToList();
                return exportLC;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateExportLCItems(ExportLCItems exportLCItems)
        {
            try
            {
                _context.ExportLCItems.Update(exportLCItems);
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
