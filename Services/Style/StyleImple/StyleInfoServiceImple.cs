using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Styles;
using CommercialManagement.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.Style.StyleImple
{
    public class StyleInfoServiceImple : StyleInfoService
    {
        private readonly CommercialDBContext _context;
        public StyleInfoServiceImple(CommercialDBContext context)
        {
            _context = context;
        }

        public bool AddStyles(StyleInfo styleInfo)
        {
            try
            {
                _context.StyleInfo.Add(styleInfo);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public StyleInfo GetbyId(int Id)
        {
            try
            {
                var StyleInfo = _context.StyleInfo
                .FirstOrDefault(s => s.ContactID == Id);
                return StyleInfo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<StyleInfoViewModel> GetStyle(string contno)
        {
            try
            {
                var param = new SqlParameter("@contno", contno);
                var styleInfoViewModel = _context.StyleInfoViewModel
                    .FromSqlRaw("EXEC usp_FindStyleInfoViaContact @contno", param)
                    .ToList();
                return styleInfoViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateStyle(StyleInfo styleInfo)
        {
            try
            {
                _context.StyleInfo.Update(styleInfo);
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
