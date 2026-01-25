using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Models.ViewModels;

namespace CommercialManagement.Services.Exports
{
    public interface ExportMainService
    {
        List<ExportMainViewModel> GetExportMain(string ExpLCName);
        //List<ExportMain> GetExportMainLCs();
        bool AddExportMain(ExportMain exportMain);
        bool UpdateExportMain(ExportMain exportMain);
        ExportMainViewModel GetbyId(int Id);
    }
}
