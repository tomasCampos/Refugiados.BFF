using System.Collections.Generic;
using System.Linq;

namespace Refugiados.BFF.Models.Requisicoes.Usuario
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
