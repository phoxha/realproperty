using System;

namespace RealProperty.Model.Auth
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
