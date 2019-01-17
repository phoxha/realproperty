using RealProperty.Model.Common;
using RealProperty.Model.Services.Absctract;
using RealProperty.Model.Users;
using RealProperty.Web.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace RealProperty.Web.Api.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route(Routes.Users)]
        public ApiResponse GetUsers(BaseFilter filter)
        {
            SearchResult<UserGridModel> users = _userService.GetUsers(filter);
            return new ApiResponse<SearchResult<UserGridModel>>(users);
        }
    }
}
