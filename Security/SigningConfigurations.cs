/*
* Arquivo de configuração dos métodos de login. 
* Define o provinder de criptografia (RsaSha256 = Chave de 256 bits)
*/
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace DesafioGlobaltec.Security {
    public class SigningConfigurations {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations() {
            using (var provider = new RSACryptoServiceProvider(2048)) {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature
            );
        }
    }
}