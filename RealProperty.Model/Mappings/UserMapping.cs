using RealProperty.Data.Entities;
using RealProperty.Model.Users;
using AutoMapper;

namespace RealProperty.Model.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserGridModel>();
        }
    }
}
