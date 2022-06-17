/*
* Controla as rotas de cadastro de pessoas
* Cria e gerencia a rota de inclusão, alteração, listagem e remoção de um cadastro.
* Optei por descrever em varias rotas cada contexto para que ficasse bem claro a ação de cada uma
* Outra forma de aplicar as rotas, seria criar uma roteirização por variável, mas poderia ficar menos organizado.
* - Metodos de Inclusão utilizam POST;
* - Métodos de Alteração utilizam PUT;
* - Métodos de Listagem utilizam GET;
* - Métodos de Remoção utilizam DELETE;
*/
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DesafioGlobaltec.Domain.Services;
using DesafioGlobaltec.Domain.Models;

namespace DesafioGlobaltec.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class Pessoas : ControllerBase {
        private SPessoa _service;

        public Pessoas(SPessoa service) {
            _service = service;
        }

        [HttpGet("Lista")]
        public IEnumerable<Pessoa> GetTodos() {
            return _service.ListarTodos();
        }

        [HttpGet("Codigo/{codigoPessoa}")]
        public ActionResult<Pessoa> GetCodigo(string codigoPessoa) {
            var Pessoa = _service.ObterPorCodigo(codigoPessoa);
            if (Pessoa != null) {
                return Pessoa;
            } else {
                return NotFound();
            }
        }

        [HttpGet("UF/{ufPessoa}")]
        public ActionResult<Pessoa> GetUF(string ufPessoa) {
            var Pessoa = _service.ObterPorUF(ufPessoa);
            if (Pessoa != null) {
                return Pessoa;
            } else {
                return NotFound();
            }
        }        

        [HttpPost("Incluir")]
        public Resultado Post([FromBody]Pessoa pessoa) {
            return _service.Incluir(pessoa);
        }

        [HttpPut("Alterar")]
        public Resultado Put([FromBody]Pessoa pessoa) {
            return _service.Atualizar(pessoa);
        }

        [HttpDelete("Excluir/{codigoPessoa}")]
        public Resultado Delete(string codigoPessoa) {
            return _service.Excluir(codigoPessoa);
        }
    }
}