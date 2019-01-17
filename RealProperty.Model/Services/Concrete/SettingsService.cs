using RealProperty.Model.Services.Absctract;
using RealProperty.Model.Settings;

namespace RealProperty.Model.Services.Concrete
{
    public class SettingsService : ISettingsService
    {
        public JwtSettings JwtSettings { get; set; }

        public SettingsService(JwtSettings jwtSettings)
        {
            JwtSettings = jwtSettings;
        }
    }
}
