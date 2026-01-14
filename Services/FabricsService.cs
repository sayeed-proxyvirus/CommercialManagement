using CommercialManagement.Models.Initial_Stage;

namespace CommercialManagement.Services
{
    public interface FabricsService
    {
        List<Fabrics> GetFabrics();
        bool AddFabrics(Fabrics fabrics);
        bool UpdateFabrics(Fabrics fabrics);
        Fabrics GetbyId(int Id);

    }
}
