using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Requisicoes
{
    public class AtualizarUsuarioRequestModel
    {
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public int CodigoUsuario { get; set; }
    }
}
