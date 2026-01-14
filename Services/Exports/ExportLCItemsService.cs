using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.Initial_Stage;

namespace CommercialManagement.Services.Exports
{
    public interface ExportLCItemsService
    {
        List<ExportLCItems> GetExportLCItems(string ExpLCNo);
        bool AddExportLCItems(ExportLCItems exportLCItems);
        bool UpdateExportLCItems(ExportLCItems exportLCItems);
        ExportLCItems GetbyId(int Id);
    }
}
