using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Requisicoes
{
    interface IRequestModel
    {
        public ValidacaoRequisicaoModel Validar();
    }
}
