using CommercialManagement.Models.Initial_Stage;

namespace CommercialManagement.Services
{
    public interface NotifyingPartyService
    {
        List<NotifyingParty> GetNotifyingParty();
        bool AddNotifyingParty(NotifyingParty notifyingParty);
        bool UpdateNotifyingParty(NotifyingParty notifyingParty);
        NotifyingParty GetbyId(int Id);
    }
}
