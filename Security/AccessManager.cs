/*
* Arquivo do Gerenciador de Acesso 
* - Realiza a validação do usuario;
* - Gera o token de autenticação;
*/
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using DesafioGlobaltec.Domain.Models;

namespace DesafioGlobaltec.Security {
    public class AccessManager {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public AccessManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public bool ValidateCredentials(User user) {
            bool credenciaisValidas = false;
            if (user != null && !String.IsNullOrWhiteSpace(user.Usuario)) {
                /// <Sumary>
                /// Verifica a existência do usuário nas tabelas do
                /// ASP.NET Core Identity
                /// </Sumary>
                /// <param name="Usuario">string UserID sem formatação</param>
                var userIdentity = _userManager.FindByNameAsync(user.Usuario).Result;

                if (userIdentity != null) {
                    /// <Sumary>
                    /// Efetua o login com base no Id do usuário e sua senha
                    /// </Sumary>
                    var resultadoLogin = _signInManager
                        .CheckPasswordSignInAsync(userIdentity, user.Senha, false)
                        .Result;

                    if (resultadoLogin.Succeeded) {
                        /// <Sumary>
                        /// Verifica se o usuário em questão possui
                        /// a role Acesso-APIPessoas
                        /// </Sumary>
                        credenciaisValidas = _userManager.IsInRoleAsync(
                            userIdentity, 
                            Roles.ROLE_API_PESSOAS
                        ).Result;
                    }
                }
            }

            return credenciaisValidas;
        }

        public Token GenerateToken(User user) {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(
                    user.Usuario, 
                    "Login"
                ),
                new[] {
                    new Claim(
                        JwtRegisteredClaimNames.Jti, 
                        Guid.NewGuid().ToString("N")
                    ),
                    new Claim(
                        JwtRegisteredClaimNames.UniqueName, 
                        user.Usuario
                    )
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(
                new SecurityTokenDescriptor {
                    Issuer = _tokenConfigurations.Issuer,
                    Audience = _tokenConfigurations.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                }
            );
            
            var token = handler.WriteToken(securityToken);

            return new Token() {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
    }
}