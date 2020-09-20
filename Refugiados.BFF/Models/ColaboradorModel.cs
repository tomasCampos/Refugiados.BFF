using System;

namespace Refugiados.BFF.Models
{
    public class ColaboradorModel
    {
        public int CodigoColaborador { get; set; }
        public string EmailContato { get; set; }
        public string NomeColaborador { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int CodigoUsuario { get; set; }
    }
}
