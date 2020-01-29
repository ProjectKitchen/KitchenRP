using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KitchenRP.DataAccess;
using KitchenRP.DataAccess.Models;
using KitchenRP.DataAccess.Queries;
using KitchenRP.DataAccess.Repositories;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;
using NodaTime;

namespace KitchenRP.Domain.Services.Internal
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservations;
        private readonly IUserRepository _users;
        private readonly IResourceRepository _resources;
        private readonly IReservationStatusRepository _statuses;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservations, IUserRepository users,
            IResourceRepository resources, IReservationStatusRepository statuses, IMapper mapper)
        {
            _reservations = reservations;
            _users = users;
            _resources = resources;
            _statuses = statuses;
            _mapper = mapper;
        }

        public async Task<List<DomainReservation>> QueryReservations(QueryReservationCommand cmd)
        {
            var query = new ReservationQuery();
            if (cmd.UserId.HasValue) query.ForOwner(await _users.FindById(cmd.UserId.Value));
            if (cmd.ResourceId.HasValue) query.ForResource(await _resources.FindById(cmd.ResourceId.Value));

            query.CollideWith(
                cmd.StartTime.GetValueOrDefault(Instant.FromUnixTimeMilliseconds(0)),    
                cmd.EndTime.GetValueOrDefault(Instant.MaxValue)
            );

            if (cmd.StatusList != null && cmd.StatusList.Length > 0)
            {
                query.WithStatuses((await _statuses.All()).Where(st => cmd.StatusList.Contains(st.Status)));
            }

            return (await _reservations.Query(query))
                .Select(r => _mapper.Map<DomainReservation>(r))
                .ToList();
        }

        public async Task<DomainReservation> AddNewReservation(AddReservationCommand cmd)
        {
            // User must exist
            var user = await _users.FindById(cmd.UserId)
                       ?? throw new EntityNotFoundException(nameof(User), $"id == {cmd.UserId}");

            // Resource must exist
            var resource = await _resources.FindById(cmd.ResourceId)
                           ?? throw new EntityNotFoundException(nameof(Resource), $"id == {cmd.ResourceId}");

            var collisions = await GetCollisionsFor(resource, cmd.StartTime, cmd.EndTime);
            // there are already Reservations for the specified time slot
            if (collisions.Count != 0) throw new ReservationCollisionException();

            var reservation = await CreateReservationWithPendingStatus(user, resource, cmd);

            return _mapper.Map<DomainReservation>(reservation);
        }
    
        private async Task<ICollection<Reservation>> GetCollisionsFor(Resource resource, Instant start, Instant end)
        {
            var statuses = await _statuses.All();
            statuses.RemoveAll(s => s.Status == DomainReservationStatus.Denied);

            var query = new ReservationQuery()
                .ForResource(resource)
                .WithStatuses(statuses)
                .CollideWith(start, end);

            return await _reservations.Query(query);
        }

        private async Task<Reservation> CreateReservationWithPendingStatus(User user, Resource resource,
            AddReservationCommand reservation)
        {
            var status = DomainReservationStatus.ReservationStatuses[DomainReservationStatus.Pending];
            var newReservation = await _reservations
                .CreateNewReservation(reservation.StartTime, reservation.EndTime, resource, user,
                    reservation.AllowNotifications);

            await ChangeStatus(new ChangeStatusCommand
            {
                Reason = "Created by user",
                ChangedBy = user,
                ChangeTo = status,
                Reservation = _mapper.Map<DomainReservation>(newReservation)
            });

            return newReservation;
        }

        public async Task ChangeStatus(ChangeStatusCommand cmd)
        {
            var newStatus = await _statuses.ByStatus(DomainReservationStatus.Pending);
            var reservation = await _reservations.FindById(cmd.Reservation.Id);
            await _reservations.CreateNewStatusChange(cmd.Reason, reservation, cmd.ChangedBy, newStatus);
        }
    }
}