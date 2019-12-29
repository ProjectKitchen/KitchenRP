using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Queries;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;
using NodaTime;

namespace KitchenRP.Domain.Services.Internal
{
    public class ReservationService: IReservationService
    {
        private readonly IReservationRepository _reservations;
        private readonly IUserRepository _users;
        private readonly IResourceRepository _resources;
        private readonly IReservationStatusRepository _statuses;
        private readonly IMapper _mapper;
        public ReservationService(IReservationRepository reservations, IUserRepository users, IResourceRepository resources, IReservationStatusRepository statuses, IMapper mapper)
        {
            _reservations = reservations;
            _users = users;
            _resources = resources;
            _statuses = statuses;
            _mapper = mapper;
        }

        public async Task<DomainReservation> AddNewReservation(AddReservationCommand r)
        {
            var user = await _users.FindById(r.UserId);
            var resource = await _resources.FindById(r.ResourceId);
            var statuses = await _statuses.All();
                statuses.RemoveAll(s => s.Status == DomainReservationStatus.Denied);

            var query = new ReservationQuery()
                .ForResource(resource)
                .WithStatuses(statuses)
                .CollideWith(r.StartTime, r.EndTime);
            
            var collisions = await _reservations.Query(query);

            if (collisions.Count != 0) throw new ReservationCollisionException();

            var status = DomainReservationStatus.ReservationStatuses[DomainReservationStatus.Pending];

            var reservation = await _reservations
                .CreateNewReservation(r.StartTime, r.EndTime, resource, user, r.AllowNotifications);

            await ChangeStatus(new ChangeStatusCommand
            {
                Reason = "Created by user",
                ChangedBy = user,
                ChangeTo = status,
                Reservation = _mapper.Map<DomainReservation>(reservation)
            });
            
            
            return _mapper.Map<DomainReservation>(reservation);
            
        }

        public async Task ChangeStatus(ChangeStatusCommand cmd)
        {
            var newStatus = await _statuses.ByStatus(DomainReservationStatus.Pending);
            var reservation = await _reservations.FindById(cmd.Reservation.Id);
            await _reservations.CreateNewStatusChange(cmd.Reason, reservation, cmd.ChangedBy, newStatus);
        }

    }
}