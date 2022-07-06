
namespace TokenGeneratorAPI.ConfigurationModels
{
    public class JwtToken
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string TokenExpiry { get; set; }
    }
}
