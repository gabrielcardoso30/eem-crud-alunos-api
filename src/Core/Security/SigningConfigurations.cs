using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            /*
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }
            */
            
            const string secretKey = "0aff096fe183ef0b85d6fa65b0029f5e51c9f545b877374a36fb3483941592dc";

            Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            //SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}