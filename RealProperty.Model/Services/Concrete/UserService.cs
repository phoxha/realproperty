using RealProperty.Data.Repositories.Abstract;
using RealProperty.Model.Common;
using RealProperty.Model.Extensions;
using RealProperty.Model.Services.Absctract;
using RealProperty.Model.Users;
using AutoMapper;
using System.Linq;

namespace RealProperty.Model.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public SearchResult<UserGridModel> GetUsers(BaseFilter filter)
        {
            var users = _userRepository.GetReadonly().FilterUsers(filter).Select(x => _mapper.Map<UserGridModel>(x));
            var searchResult = users.BaseFilter(filter);

            return searchResult;
        }
    }
}
