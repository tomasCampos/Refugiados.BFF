using System;
using FluentValidation;

namespace Refugiados.BFF.Models
{
    public class EmpresaModel
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoUsuario { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }

    public class EmpresaValidator : AbstractValidator<EmpresaModel>
    {
        public EmpresaValidator()
        {
            RuleFor(empresa => empresa.RazaoSocial)
                .NotEmpty().WithMessage("O campo nome da empresa deve ser preenchido")
                .NotNull().WithMessage("O nome da empresa não pode ser nulo");

            RuleFor(empresa => empresa.CodigoUsuario)
                .NotNull().WithMessage("O código de usuário não pode ser nulo");
        }
    }
}
