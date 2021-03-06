using System.Collections.Generic;
using System.Threading.Tasks;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;

namespace KitchenRP.Domain.Services
{
    public interface IReservationService
    {
        Task<DomainReservation> AddNewReservation(AddReservationCommand cmd);
        Task<List<DomainReservation>> QueryReservations(QueryReservationCommand cmd);
        Task<DomainReservation> AcceptReservation(AcceptReservationCommand cmd);
        Task<DomainReservation> DenyReservation(DenyReservationCommand cmd);
        Task DeleteReservation(DeleteReservationCommand cmd);
    }
}