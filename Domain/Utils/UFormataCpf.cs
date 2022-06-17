/*
* Arquivo utilitário para aplicação de máscara no CPF.
*/
using System;

namespace DesafioGlobaltec.Domain.Utils { 
    public static class FormatCpf {
        // Esse método aplica a máscara no CPF
        public static string MascaraCPF(string CPF) {
            if (CPF.Trim() != null && CPF != "" ) {
                // Antes de aplicar a máscara, são removidos quaisquer caracteres 
                // não numéricos que possam ter sido informados na chamada da API
                CPF = CPF.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);                

                // Depois é realizada a aplicação da máscara
                return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
            } else {
                return "";
            };
        }
        // Esse método remove a máscara do CPF de forma a realizar validações e gravar os valores sem máscara
        public static string SemFormatacao(string CPF) {
            return CPF.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
    }
}