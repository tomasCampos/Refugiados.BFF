using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Requisicoes
{
    public class CadastrarUsuarioRequestModel
    {
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
    }
}
