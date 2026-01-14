using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.ExportInvoice;
using CommercialManagement.Models.Initial_Stage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.Exports.ExportsServiceImple
{
    public class ExportDataServiceImple : ExportDataService
    {
        private readonly CommercialDBContext _context;
        public ExportDataServiceImple(CommercialDBContext context)
        {
            _context = context;
        }
        public bool AddExportData(ExportData exportData)
        {
            try
            {
                _context.ExportData.Add(exportData);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ExportData GetbyId(int Id)
        {
            try
            {
                var exportData = _context.ExportData
                    .FirstOrDefault(c => c.ContactID == Id);
                return exportData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ExportData> GetExportData(string ExpInv)
        {
            try
            {
                var param = new SqlParameter("@ExpInv", ExpInv);
                var exportData = _context.ExportData
                    .FromSqlRaw("EXEC usp_FindExpData @ExpInv", param)
                    .ToList();
                return exportData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateExportData(ExportData exportData)
        {
            try
            {
                _context.ExportData.Update(exportData);
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
