using Refugiados.BFF.Models.Requisicoes;
using Refugiados.BFF.Models.Requisicoes.Endereco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Refugiados.BFF.Models.Colaborador.Requisicoes
{
    public class AtualizarColaboradorRequestModel : IRequestModel
    {
        public string NomeColaborador { get; set; }
        public string Escolaridade { get; set; }
        public string Nacionalidade { get; set; }
        public string AreaFormacao { get; set; }
        public DateTime? DataChegadaBrasil { get; set; }
        public DateTime? DataNascimento { get; set; }
        public List<int> Idiomas { get; set; }
        public List<int> AreasTrabalho { get; set; }
        public CadastrarAtualizarEnderecoRequestModel Endereco { get; set; }

        public ValidacaoRequisicaoModel Validar()
        {
            var erros = new List<string>();
            if (string.IsNullOrWhiteSpace(NomeColaborador) && string.IsNullOrWhiteSpace(Escolaridade) && string.IsNullOrWhiteSpace(Nacionalidade) && string.IsNullOrWhiteSpace(AreaFormacao) &&
                !DataChegadaBrasil.HasValue && !DataNascimento.HasValue && Idiomas == null && AreasTrabalho == null && Endereco == null) 
            {
                erros.Add("Nenhum dado para atualizar");
            }

            return new ValidacaoRequisicaoModel { Erros = erros, Valido = !erros.Any() };
        }
    }
}
