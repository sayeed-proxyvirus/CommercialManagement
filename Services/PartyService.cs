using CommercialManagement.Models.Initial_Stage;

namespace CommercialManagement.Services
{
    public interface PartyService
    {
        List<Party> GetParty();
        bool AddParty(Party party);
        bool UpdateParty(Party party);
        Party GetbyId(int Id);
    }
}
