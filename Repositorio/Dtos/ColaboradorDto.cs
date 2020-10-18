using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Dtos
{
    public class ColaboradorDto
    {
        public int codigo_colaborador { get; set; }
        public string nome_colaborador { get; set; }
        public string nacionalidade { get; set; }
        public string area_formacao { get; set; }
        public DateTime? data_nascimento { get; set; }
        public DateTime? data_chegada_brasil { get; set; }
        public string escolaridade { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_alteracao { get; set; }
        public int codigo_usuario { get; set; }
        public string email_usuario { get; set; }
        public string telefone_usuario { get; set; }
        public bool entrevistado { get; set; }
    }
}
