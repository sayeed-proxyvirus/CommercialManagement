using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Styles;
using CommercialManagement.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CommercialManagement.Services.Style.StyleImple
{
    public class StyleTransServiceImple : StyleTransService
    {
        private readonly CommercialDBContext _context;
        public StyleTransServiceImple(CommercialDBContext context)
        {
            _context = context;
        }

        public bool AddStyles(StyleTrans styleTrans)
        {
            try
            {
                _context.StyleTrans.Add(styleTrans);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public StyleTrans GetbyId(int Id)
        {
            try
            {
                var StyleTrans = _context.StyleTrans
                .FirstOrDefault(s => s.ContactID == Id);
                return StyleTrans;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<StyleTransViewModel> GetStyleTrans(string contno)
        {
            try
            {
                var param = new SqlParameter("@contno", contno);
                var styleTransVM = _context.StyleTransViewModel
                    .FromSqlRaw("EXEC usp_FindStyleTransViaContact @contno", param)
                    .ToList();
                return styleTransVM;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateStyle(StyleTrans styleTrans)
        {
            try
            {
                _context.StyleTrans.Update(styleTrans);
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
