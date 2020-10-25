using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Requisicoes.Endereco
{
    public class CadastrarAtualizarEnderecoRequestModel
    {
        public string CidadeEndereco { get; set; }
        public string BairroEndereco { get; set; }
        public string RuaEndereco { get; set; }
        public string NumeroEndereco { get; set; }
        public string ComplementoEndereco { get; set; }
        public string CepEndereco { get; set; }
        public string EstadoEndereco { get; set; }
    }
}
