using CommercialManagement.Models.Styles;
using CommercialManagement.Models.ViewModels;

namespace CommercialManagement.Services.Style
{
    public interface StyleInfoService
    {
        bool AddStyles(StyleInfo styleInfo);
        bool UpdateStyle(StyleInfo styleInfo);
        StyleInfo GetbyId(int Id);
        List<StyleInfoViewModel> GetStyle(string contno);
    }
}
