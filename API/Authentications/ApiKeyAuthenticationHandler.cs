using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace API.Authentications
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        #region Variables Declaration
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructors
        /// <summary>
        /// ApiKeyAuthenticationHandler class constructor
        /// </summary>
        /// <param name="options">Options monitor interface</param>
        /// <param name="logger">Loffer factory interface</param>
        /// <param name="encoder">Url encoder</param>
        /// <param name="clock">System clock interface</param>
        /// <param name="configuration">Configuration interface</param>
        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Overrides the HandleAuthenticateAsync method
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var apiKeyValue))
            {
                await Context.Response.WriteAsync("No API key provided");
                return AuthenticateResult.Fail("No API key provided");
            }

            var apiKey = _configuration.GetValue<string>("ApiKey");

            if (apiKeyValue != apiKey)
            {
                await Context.Response.WriteAsync("Invalid API key");
                return AuthenticateResult.Fail("Invalid API key");
            }

            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "API User") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        } 
        #endregion
    }
}
