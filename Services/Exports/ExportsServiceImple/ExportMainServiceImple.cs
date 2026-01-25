using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.ExpLC;
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
            throw new NotImplementedException();
        }

        public ExportMainViewModel GetbyId(int Id)
        {
            try
            {
                var exportMainViewModel = _context.ExportMainViewModel
                    .FirstOrDefault(c => c.ExpID == Id);
                return exportMainViewModel;
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
            throw new NotImplementedException();
        }
        
    }
}
