using AutoMapper;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;
using NodaTime;
using NodaTime.Text;

namespace KitchenRP.Web.Models
{
    public class KitchenRpWebMappings : Profile
    {
        public KitchenRpWebMappings()
        {
            CreateMap<DomainUser, UserResponse>();
            CreateMap<AddResourceRequest, AddResourceCommand>();
            CreateMap<AddResourceTypeRequest, AddResourceTypeCommand>();
            CreateMap<AuthRequest, AuthCommand>();
            CreateMap<UserActivationRequest, ActivateUserCommand>();
            CreateMap<AddReservationRequest, AddReservationCommand>();
            
            CreateMap<QueryReservationRequest, QueryReservationCommand>()
                .ForMember(q => q.EndTime, 
                    opt => opt.MapFrom(
                        q => InstantPattern.ExtendedIso.Parse(q.EndTime).Value))
                .ForMember(q => q.StartTime, 
                    opt => opt.MapFrom(
                        q => InstantPattern.ExtendedIso.Parse(q.StartTime).Value));
            
            CreateMap<DomainReservation, ReservationResponse>()
                .ForMember(r => r.EndTime,
                    opt =>
                        opt.MapFrom(src => src.ReservedFor.End.ToString()))
                .ForMember(r => r.StartTime,
                    opt =>
                        opt.MapFrom(src => src.ReservedFor.Start.ToString()));
        }
    }
}