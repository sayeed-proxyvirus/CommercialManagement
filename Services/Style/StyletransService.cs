using CommercialManagement.Models.Styles;
using CommercialManagement.Models.ViewModels;

namespace CommercialManagement.Services.Style
{
    public interface StyleTransService
    {
        bool AddStyles(StyleTrans styleTrans);
        bool UpdateStyle(StyleTrans styleTrans);
        StyleTrans GetbyId(int Id);
        List<StyleTransViewModel> GetStyleTrans(string contno);
    }
}
