using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.Exports.ExportsServiceImple
{
    public class ExportMainServiceImple : ExportMainService
    {
        private readonly CommercialDBContext _context;
        public ExportMainServiceImple(CommercialDBContext context)
        {
            _context = context;
        }

        public bool AddExportMain(ExportMain exportMain)
        {
            try
            {
                _context.ExportMain.Add(exportMain);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ExportMain GetbyId(int Id)
        {
            try
            {
                var exportMain = _context.ExportMain
                    .FirstOrDefault(c => c.ExpID == Id);
                return exportMain;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ExportMainViewModel> GetExportMain(string ExpLCName)
        {
            try
            {
                var param = new SqlParameter("@ExpLCName", ExpLCName);
                var exportMainViewModel = _context.ExportMainViewModel
                    .FromSqlRaw("EXEC usp_FindLCDataMains @ExpLCName", param)
                    .ToList();
                return exportMainViewModel;
            }
            catch (Exception)
            {
                throw; ;
            }
        }


        public bool UpdateExportMain(ExportMain exportMain)
        {
            try
            {
                _context.ExportMain.Update(exportMain);
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
