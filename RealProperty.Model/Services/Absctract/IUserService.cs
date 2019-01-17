using RealProperty.Model.Common;
using RealProperty.Model.Users;

namespace RealProperty.Model.Services.Absctract
{
    public interface IUserService
    {
        SearchResult<UserGridModel> GetUsers(BaseFilter filter);
    }
}
