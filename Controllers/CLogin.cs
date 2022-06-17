/*
* Controle da rota de Login:
*
* Esse controler realizar a roteirização do login do usuario.
* Além de utilizar o método post, que oculta os dados de login, utiliza a validação de credenciais, 
* verificando o usuário e retornando o token de autenticação para as demais rotas
*/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DesafioGlobaltec.Security;

namespace DesafioGlobaltec.Controllers {
    [Route("api/[controller]")]
    public class Login : Controller {
        [AllowAnonymous]
        [HttpPost]
        public object Post(
            [FromBody]User usuario,
            [FromServices]AccessManager accessManager
        ) {
            if (accessManager.ValidateCredentials(usuario)) {
                return accessManager.GenerateToken(usuario);
            } else {
                return new {
                    Authenticated = false,
                    Message = "Falha ao autenticar"
                };
            }
        }
    }
}