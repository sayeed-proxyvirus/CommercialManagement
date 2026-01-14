using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Initial_Stage;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.ServiceImple
{
    public class NotifyingPartyServiceImple : NotifyingPartyService
    {
        private readonly CommercialDBContext _context;
        public NotifyingPartyServiceImple(CommercialDBContext context)
        {
            _context = context;
        }

        public NotifyingParty GetbyId(int Id)
        {
            try
            {
                var notifyingParty = _context.NotifyingParty
                    .FirstOrDefault(c => c.PartyID == Id);
                return notifyingParty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<NotifyingParty> GetNotifyingParty()
        {
            try
            {
                var notifyingParty = _context.NotifyingParty
                    .FromSqlRaw("EXEC usp_ViewNotParty")
                    .ToList();
                return notifyingParty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddNotifyingParty(NotifyingParty notifyingParty)
        {
            try
            {
                _context.NotifyingParty.Add(notifyingParty);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateNotifyingParty(NotifyingParty notifyingParty)
        {
            try
            {
                _context.NotifyingParty.Update(notifyingParty);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
