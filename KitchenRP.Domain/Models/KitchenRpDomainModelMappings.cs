using System;
using System.Collections.Generic;
using AutoMapper;
using KitchenRP.DataAccess.Models;
using NodaTime;

namespace KitchenRP.Domain.Models
{
    public class KitchenRpDomainModelMappings : Profile
    {
        public KitchenRpDomainModelMappings()
        {
            CreateMap<User, DomainUser>()
                .ForCtorParam("role", cfg => cfg.MapFrom(u => u.Role.RoleName));
            CreateMap<RefreshToken, DomainRefreshToken>();


            CreateMap<ReservationStatus, DomainReservationStatus>()
                .ConvertUsing(rs =>
                    DomainReservationStatus.ReservationStatuses.GetValueOrDefault(rs.Status)!
                );

            CreateMap<StatusChange, DomainStatusChange>();

            CreateMap<ResourceType, DomainResourceType>();

            CreateMap<Resource, DomainResource>();

            CreateMap<Reservation, DomainReservation>()
                .ForMember(r => r.ReservedFor,
                    opt =>
                        opt.MapFrom(src => new Interval(src.StartTime, src.EndTime)));

        }
    }
}