/*
* Arquivo contendo o modelo do cadastro de pessoas.
* Note que, já durante a modelagem dos dados, as mascaras e tipos são tratadas 
* independentemente da forma como foram informadas no JSON na chamada da API
*/
using System;
using DesafioGlobaltec.Domain.Utils;

namespace DesafioGlobaltec.Domain.Models {
    public class Pessoa {
        private string _CodPessoa;
        public string CodigoPessoa {
            get => _CodPessoa;
            set => _CodPessoa = value?.Trim();
        }

        private string _nomePessoa;
        public string NomePessoa {
            get => _nomePessoa;
            set => _nomePessoa = value?.Trim().ToUpper();
        }

        private string _cpfPessoa;
        public string CPFPessoa {
            get => FormatCpf.MascaraCPF(_cpfPessoa);
            set => _cpfPessoa = FormatCpf.SemFormatacao(value?.Trim());
        }

        private string _ufPessoa;
        public string UFPessoa {
            get => _ufPessoa;
            set => _ufPessoa = value?.Trim().ToUpper();
        }

        private DateTime _dtNascimentoPessoa;
        public string DtNascimentoPessoa {
            get => _dtNascimentoPessoa.ToString("dd/MM/yyyy");
            set => _dtNascimentoPessoa = DateTime.ParseExact(value.Trim().ToString(), "dd/MM/yyyy", null);
        }
    }
}