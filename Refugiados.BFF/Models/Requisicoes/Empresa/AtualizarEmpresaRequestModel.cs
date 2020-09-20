using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Requisicoes.Empresa
{
    public class AtualizarEmpresaRequestModel
    {
        public string RazaoSocial { get; set; }
    }
}
