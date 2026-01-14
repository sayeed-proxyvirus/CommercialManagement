using CommercialManagement.Models.Initial_Stage;

namespace CommercialManagement.Services
{
    public interface BeneficiaryService
    {
        List<Beneficiary> GetBeneficiary();
        bool AddBeneficiary(Beneficiary beneficiary);
        bool UpdateBeneficiary(Beneficiary beneficiary);
        Beneficiary GetbyId(int Id);
    }
}
