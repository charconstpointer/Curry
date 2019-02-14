using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Curry.Models.User;

namespace Curry.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserBindingModel>()
                .ForMember(ubm => ubm.Roles,
                    opt => opt.MapFrom
                        (r => r.UserRoles.Select(ur => ur.RoleId)));

            CreateMap<User, UserResourceModel>()
                .ForMember(ubm => ubm.Roles, opt => opt.MapFrom(u => u.UserRoles.Select(ur => ur.Role)));

            CreateMap<UserBindingModel, User>()
                .ForMember(u => u.UserRoles,
                    opt => opt.MapFrom
                    (ur => ur.Roles.Select(id => new UserRole {RoleId = id})));
        }
    }

    public class UserResourceModel
    {
        public string Username { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}