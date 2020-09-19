using System;
using System.Collections.Generic;
using System.Text;

namespace Repositorio.Dtos
{
    public class UsuarioDto
    {
        public int codigo_usuario { get; set; }
        public string email_usuario { get; set; }
        public string senha_usuario { get; set; }
        public DateTime data_criacao { get; set; }
        public DateTime data_alteracao { get; set; }
        public int? perfil_usuario { get; set; }
    }
}
