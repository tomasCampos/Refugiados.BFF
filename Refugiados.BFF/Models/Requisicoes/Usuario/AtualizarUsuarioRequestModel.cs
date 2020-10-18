using Refugiados.BFF.Models.Requisicoes;
using System.Collections.Generic;
using System.Linq;

namespace Refugiados.BFF.Models.Usuario.Requisicoes
{
    public class AtualizarUsuarioRequestModel : IRequestModel
    {
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string TelefoneUsuario { get; set; }
        public bool? Entrevistado { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(EmailUsuario) && string.IsNullOrWhiteSpace(SenhaUsuario) && !Entrevistado.HasValue && string.IsNullOrEmpty(TelefoneUsuario))
                erros.Add("Nenhum dado para atualizar");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }
    }
}
