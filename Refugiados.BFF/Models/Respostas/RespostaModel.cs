using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Respostas
{
    public class RespostaModel
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Corpo { get; set; }
    }
}
