using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Dtos
{
    public class EnderecoDto
    {
        public int codigo_endereco { get; set; }
        public string cidade_endereco { get; set; }
        public string bairro_endereco { get; set; }
        public string rua_endereco { get; set; }
        public string numero_endereco { get; set; }
        public string complemento_endereco { get; set; }
        public string cep_endereco { get; set; }
        public string estado_endereco { get; set; }
    }
}
