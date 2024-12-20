using AutoMapper;
using Zopoesht.Application.Users.Dtos;
using Zopoesht.Data.Users;

namespace Zopoesht.Application.Infrastructure.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Dto to Model

            // Model to Dto
            this.CreateMap<Role, RoleDto>();

            this.CreateMap<User, UserSearchResultDto>()
                .ForMember(m => m.Fullname, cfg => cfg.MapFrom(src => src.FirstName + " " + src.LastName));

            this.CreateMap<User, UserDto>()
                .ForMember(m => m.Password, cfg => cfg.Ignore());
        }
    }
}
