using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenRP.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenRP.DataAccess.Repositories.Internal
{
    public class ReservationStatusRepository : IReservationStatusRepository
    {
        private readonly KitchenRpContext _context;

        public ReservationStatusRepository(KitchenRpContext context)
        {
            _context = context;
        }

        public Task<List<ReservationStatus>> All()
        {
            return _context.ReservationStatuses.ToListAsync();
        }

        public ValueTask<ReservationStatus> ById(long id)
        {
            return _context.ReservationStatuses.FindAsync(id);
        }

        public Task<ReservationStatus> ByStatus(string status)
        {
            return _context.ReservationStatuses.FirstAsync(rs => rs.Status == status);
        }
    }
}