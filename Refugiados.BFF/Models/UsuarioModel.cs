using System;

namespace Refugiados.BFF.Models
{
    public class UsuarioModel
    {
        public int Codigo { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? PerfilUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
