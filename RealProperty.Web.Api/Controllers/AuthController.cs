using RealProperty.Model.Auth;
using RealProperty.Model.Services.Absctract;
using RealProperty.Web.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RealProperty.Web.Api.Controllers
{
    public class AuthController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(Routes.AuthLogin)]
        public ApiResponse<TokenModel> Login([FromBody] LoginModel model)
        {
            TokenModel result = _authService.Login(model);
            return new ApiResponse<TokenModel>(result);
        }
    }
}
