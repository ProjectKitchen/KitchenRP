using AutoMapper;
using KitchenRP.Domain.Commands;
using KitchenRP.Domain.Models;

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
        }
    }
}