using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RealProperty.Data.Entities;
using RealProperty.Data.Repositories.Abstract;
using RealProperty.Model.Auth;
using RealProperty.Model.Services.Absctract;
using Microsoft.IdentityModel.Tokens;

namespace RealProperty.Model.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISettingsService _settingsService;

        public AuthService(IUserRepository userRepository, ISettingsService settingsService)
        {
            _userRepository = userRepository;
            _settingsService = settingsService;
        }

        public TokenModel Login(LoginModel model)
        {
            var user = _userRepository.Get(model.Login, model.Password);
            if (user != null) {
                var jwtToken = GenerateToken(user);

                return new TokenModel
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    Expires = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays)
                };
            }

            return null;
        }

        private JwtSecurityToken GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settingsService.JwtSettings.Key));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(_settingsService.JwtSettings.ExpireDays);

            return new JwtSecurityToken(
                _settingsService.JwtSettings.Issuer,
                null,
                claims,
                expires: expires,
                signingCredentials: signingCredentials
            );
        }
    }
}
