using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Dtos
{
    public class ColaboradorDto
    {
        public int codigo_colaborador { get; set; }
        public string nome_colaborador { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_alteracao { get; set; }
        public int codigo_usuario { get; set; }
    }
}
