using System;
using System.Collections.Generic;
using System.Linq;

namespace Refugiados.BFF.Models.Requisicoes.Empresa
{
    public class AtualizarEmpresaRequestModel : IRequestModel
    {
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string NomeFantasia { get; set; }
        public DateTime? DataFundacao { get; set; }
        public int? NumeroFuncionarios { get; set; }
        public List<int> AreasTrabalho { get; set; }
        public EnderecoModel Endereco { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(RazaoSocial) && string.IsNullOrWhiteSpace(CNPJ) && string.IsNullOrWhiteSpace(NomeFantasia) && !DataFundacao.HasValue && !NumeroFuncionarios.HasValue && AreasTrabalho == null)
                erros.Add("Nenhum dado para atualizar");

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }
    }
}
