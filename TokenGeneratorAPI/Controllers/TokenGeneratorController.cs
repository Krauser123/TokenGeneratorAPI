using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TokenGeneratorAPI.Classes;
using TokenGeneratorAPI.ConfigurationModels;

namespace TokenGeneratorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenGeneratorController : ControllerBase
    {
        private readonly ILogger<TokenGeneratorController> _logger;
        private readonly JwtTokenUtils JwtTokenUtils;
        private readonly IOptions<JwtToken> JwtToken;
        private readonly IOptions<AuthLogin> AuthLogin;

        public TokenGeneratorController(ILogger<TokenGeneratorController> logger, IOptions<JwtToken> jwtToken, IOptions<AuthLogin> authLogin)
        {
            _logger = logger;
                        
            JwtToken = jwtToken;
            AuthLogin = authLogin;
            JwtTokenUtils = new JwtTokenUtils(JwtToken);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] LoginModel login)
        {
            string tokenString = string.Empty;
            if (login == null) return Unauthorized();
                        
            bool validUser = Authenticate(login);
            if (validUser)
            {
                tokenString = JwtTokenUtils.BuildJWTToken();
            }
            else
            {
                return Unauthorized();
            }

            return Ok(new { Token = tokenString });
        }

        private bool Authenticate(LoginModel login)
        {
            bool validUser = false;

            if (login.Username == AuthLogin.Value.UserName && login.Password == AuthLogin.Value.Password)
            {
                validUser = true;
            }
            return validUser;
        }
    }
}
