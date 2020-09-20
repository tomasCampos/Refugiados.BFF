using Refugiados.BFF.Models.Requisicoes;
using System.Collections.Generic;
using System.Linq;

namespace Refugiados.BFF.Models.Usuario.Requisicoes
{
    public class UsuarioRequestModel : IRequestModel
    {
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(EmailUsuario))
                erros.Add("O campo email do usuario deve ser preenchido");
            if (string.IsNullOrWhiteSpace(SenhaUsuario))
                erros.Add("O campo senha do usuario deve ser preenchido");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }
    }
}
