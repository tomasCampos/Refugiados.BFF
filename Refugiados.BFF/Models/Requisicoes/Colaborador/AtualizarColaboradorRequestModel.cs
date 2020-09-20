using Refugiados.BFF.Models.Requisicoes;
using System.Collections.Generic;
using System.Linq;

namespace Refugiados.BFF.Models.Colaborador.Requisicoes
{
    public class AtualizarColaboradorRequestModel : IRequestModel
    {
        public string NomeColaborador { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(NomeColaborador))
                erros.Add("Nenhum dado para atualizar");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }
    }
}
