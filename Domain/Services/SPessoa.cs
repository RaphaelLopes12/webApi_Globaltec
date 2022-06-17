/*
* Arquivo conténdo as validações e demais funções de manipulação dos cadastros de pessoas
* Possui os métodos responsáveis pela inclusão, listagem, alteração e exclusão de registros
* Tambémn realiza as validações dos dados informados para verificar a consistencia dos mesmos
*/
using System;
using System.Collections.Generic;
using System.Linq;
using DesafioGlobaltec.Domain.Data;
using DesafioGlobaltec.Domain.Models;
using DesafioGlobaltec.Domain.Utils;

namespace DesafioGlobaltec.Domain.Services {
    public class SPessoa {
        private CatalogoDbContext _context;

        public SPessoa(CatalogoDbContext context) {
            _context = context;
        }

        public Pessoa ObterPorCodigo(string CodigoPessoa) {
            CodigoPessoa = CodigoPessoa?.Trim().ToUpper();
            if (!String.IsNullOrWhiteSpace(CodigoPessoa)) {
                return _context.Pessoas.Where(
                    p => p.CodigoPessoa == CodigoPessoa
                ).FirstOrDefault();
            } else {
                return null;
            }
        }

        public Pessoa ObterPorUF(string ufPessoa) {
            ufPessoa = ufPessoa?.Trim().ToUpper();
            if (!String.IsNullOrWhiteSpace(ufPessoa)) {
                return _context.Pessoas.Where(
                    p => p.UFPessoa == ufPessoa
                ).FirstOrDefault();
            } else {
                return null;
            }
        }        

        public IEnumerable<Pessoa> ListarTodos() {
            return _context.Pessoas.OrderBy(p => p.NomePessoa).ToList();
        }

        public Resultado Incluir(Pessoa dadosPessoa) {
            Resultado resultado = DadosValidos(dadosPessoa);
            resultado.Acao = "Cadastro realizado com sucesso!";
            resultado.Pessoa.CodigoPessoa = dadosPessoa.CodigoPessoa;
            resultado.Pessoa.NomePessoa = dadosPessoa.NomePessoa;
            resultado.Pessoa.CPFPessoa = dadosPessoa.CPFPessoa;
            resultado.Pessoa.UFPessoa = dadosPessoa.UFPessoa;

            if (resultado.Inconsistencias.Count == 0 && _context.Pessoas.Where(
                p => p.CodigoPessoa == dadosPessoa.CodigoPessoa
            ).Count() > 0) {
                resultado.Inconsistencias.Add(
                    "Código de pessoa já cadastrado"
                );
            }

            if (resultado.Inconsistencias.Count == 0) {
                _context.Pessoas.Add(dadosPessoa);
                _context.SaveChanges();

            }

            return resultado;
        }

        public Resultado Atualizar(Pessoa dadosPessoa) {
            Resultado resultado = DadosValidos(dadosPessoa);
            resultado.Acao = "Atualização de cadastro realizada com sucesso!";

            if (resultado.Inconsistencias.Count == 0) {
                Pessoa pessoa = _context.Pessoas.Where(
                    p => p.CodigoPessoa == dadosPessoa.CodigoPessoa
                ).FirstOrDefault();

                if (pessoa == null) {
                    resultado.Inconsistencias.Add(
                        "Cadastro não encontrado"
                    );
                } else {
                    pessoa.NomePessoa = dadosPessoa.NomePessoa;
                    pessoa.CPFPessoa = dadosPessoa.CPFPessoa;
                    pessoa.UFPessoa = dadosPessoa.UFPessoa;
                    pessoa.DtNascimentoPessoa = dadosPessoa.DtNascimentoPessoa;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        public Resultado Excluir(string CodigoPessoa) {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de cadastro realizada com sucesso!";

            Pessoa pessoa = ObterPorCodigo(CodigoPessoa);
            if (pessoa == null) {
                resultado.Inconsistencias.Add(
                    "Cadastro não encontrado"
                );
            } else {
                _context.Pessoas.Remove(pessoa);
                _context.SaveChanges();
            }

            return resultado;
        }

        private Resultado DadosValidos(Pessoa pessoa) {
            var resultado = new Resultado();
            if (pessoa == null) {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do cadastro"
                );
            } else {
                if (String.IsNullOrWhiteSpace(pessoa.CodigoPessoa)) {
                    resultado.Inconsistencias.Add(
                        "Preencha o Código de cadastro"
                    );
                }

                if (String.IsNullOrWhiteSpace(pessoa.NomePessoa)) {
                    resultado.Inconsistencias.Add(
                        "Preencha o Nome da pessoa"
                    );
                }

                if (String.IsNullOrWhiteSpace(pessoa.CPFPessoa)) {
                    resultado.Inconsistencias.Add(
                        "Preencha o CPF da pessoa"
                    );
                }

                if (FormatCpf.SemFormatacao(pessoa.CPFPessoa).Length != 11 ) {
                    resultado.Inconsistencias.Add(
                        "CPF Inválido: Quantidade de digitos invalido!"
                    );
                }

                if (!ValidaCPF.IsCpf(pessoa.CPFPessoa)) {
                    resultado.Inconsistencias.Add(
                        "CPF Inválido: Dígito verificador inconsistente!"
                    );
                }

                if (String.IsNullOrWhiteSpace(pessoa.UFPessoa)) {
                    resultado.Inconsistencias.Add(
                        "Preencha a UF da pessoa"
                    );
                }

                if (String.IsNullOrWhiteSpace(pessoa.DtNascimentoPessoa)) {
                    resultado.Inconsistencias.Add(
                        "Preencha data de nascimento da pessoa"
                    );
                }
            }

            return resultado;
        }
    }
}