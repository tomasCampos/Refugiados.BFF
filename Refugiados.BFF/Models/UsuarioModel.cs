using System;

namespace Refugiados.BFF.Models
{
    public class UsuarioModel
    {
        public int CodigoUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string TelefoneUsuario { get; set; }
        public int? PerfilUsuario { get; set; }
        public bool Entrevistado { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
