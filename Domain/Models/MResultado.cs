/*
* Modelo para validação do resultado das chamadas de manipulação dos cadastros de pessoas;
* Retorna os status das ações e a estrutura informada no cadastro, inclusive, mascarando os resultados dos campos que contenham mascara;
*/
using System.Collections.Generic;

namespace DesafioGlobaltec.Domain.Models {
    public class Resultado {
        public string Acao { get; set; }

        public bool Sucesso {
            get { 
                return Inconsistencias == null || Inconsistencias.Count == 0; 
            }
        }
        public List<string> Inconsistencias { get; } = new List<string>();
        public Pessoa Pessoa { get; } = new Pessoa();
    }
}