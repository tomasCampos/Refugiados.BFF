using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models
{
    public class ColaboradorModel
    {
        public int CodigoColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int CodigoUsuario { get; set; }
    }
}
