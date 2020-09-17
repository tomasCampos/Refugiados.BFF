using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Models.Requisicoes
{
    public class UsuarioColaboradorRequestModel : UsuarioRequestModel
    {
        public string NomeColaborador { get; set; }

    }

    public class UsuarioColaboradorRequestModelValidator : AbstractValidator<UsuarioColaboradorRequestModel>
    {
        public UsuarioColaboradorRequestModelValidator()
        {
            RuleFor(usuarioColaborador => usuarioColaborador.NomeColaborador)
                .NotEmpty().WithMessage("O campo nome do colaborador deve ser preenchido")
                .NotNull().WithMessage("O nome do colaborador não pode ser nulo");
        }
    }
}
