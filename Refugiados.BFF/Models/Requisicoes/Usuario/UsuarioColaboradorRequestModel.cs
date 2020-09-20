using FluentValidation;
using Refugiados.BFF.Models.Requisicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Usuario.Requisicoes
{
    public class UsuarioColaboradorRequestModel : IRequestModel
    {
        public string NomeColaborador { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(EmailUsuario))
                erros.Add("O campo email do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(SenhaUsuario))
                erros.Add("O campo senha do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(NomeColaborador))
                erros.Add("O campo nome do colaborador deve ser preenchido");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }
    }
}
