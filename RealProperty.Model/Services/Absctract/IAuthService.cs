using RealProperty.Model.Auth;

namespace RealProperty.Model.Services.Absctract
{
    public interface IAuthService
    {
        TokenModel Login(LoginModel model);
    }
}
