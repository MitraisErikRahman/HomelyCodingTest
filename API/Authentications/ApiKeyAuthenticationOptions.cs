using Microsoft.AspNetCore.Authentication;

namespace API.Authentications
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string AuthenticationScheme { get; set; }
        public string ApiKey { get; set; }
    }
}
