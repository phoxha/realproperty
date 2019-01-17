using RealProperty.Model.Settings;

namespace RealProperty.Model.Services.Absctract
{
    public interface ISettingsService
    {
        JwtSettings JwtSettings { get; set; }
    }
}
