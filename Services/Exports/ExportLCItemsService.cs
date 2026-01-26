using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Models.ViewModels;

namespace CommercialManagement.Services.Exports
{
    public interface ExportLCItemsService
    {
        List<ExportLCViewModel> GetExportLCItems(string ExpLCNo);
        bool AddExportLCItems(ExportLCItems exportLCItems);
        bool UpdateExportLCItems(ExportLCItems exportLCItems);
        ExportLCItems GetbyId(int Id);
    }
}
