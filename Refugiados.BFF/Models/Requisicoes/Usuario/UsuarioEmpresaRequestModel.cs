using FluentValidation;
using Refugiados.BFF.Models.Usuario.Requisicoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Requisicoes.Usuario
{
    public class UsuarioEmpresaRequestModel : UsuarioRequestModel
    {
        public string RazaoSocial { get; set; }
    }

    public class UsuarioEmpresaRequestModelValidator : AbstractValidator<UsuarioEmpresaRequestModel>
    {
        public UsuarioEmpresaRequestModelValidator()
        {
            RuleFor(empresa => empresa.RazaoSocial)
                .NotEmpty().WithMessage("O campo razão social deve ser preenchido")
                .NotNull().WithMessage("A razão social da empresa não pode ser nula");
        }
    }
}
