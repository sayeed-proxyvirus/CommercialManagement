using CommercialManagement.Models.Initial_Stage;

namespace CommercialManagement.Services
{
    public interface ApplicantConsigneesService
    {
        List<ApplicantConsignees> GetApplicantConsignees();
        bool AddApplicantConsignees(ApplicantConsignees applicantConsignees);
        bool UpdateApplicantConsignees(ApplicantConsignees applicantConsignees);
        ApplicantConsignees GetbyId(int Id);
    }
}
