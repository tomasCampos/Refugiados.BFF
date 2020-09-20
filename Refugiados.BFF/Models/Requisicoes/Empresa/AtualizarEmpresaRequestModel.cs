using System.Collections.Generic;
using System.Linq;

namespace Refugiados.BFF.Models.Requisicoes.Empresa
{
    public class AtualizarEmpresaRequestModel : IRequestModel
    {
        public string RazaoSocial { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(RazaoSocial))
                erros.Add("Nenhum dado para atualizar");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }
    }
}
