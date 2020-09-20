using FluentValidation;

namespace Refugiados.BFF.Models.Usuario.Requisicoes
{
    public class UsuarioRequestModel
    {
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
    }

    public class UsuarioRequestModelValidator : AbstractValidator<UsuarioRequestModel>
    {
        public UsuarioRequestModelValidator()
        {
            RuleFor(usuario => usuario.EmailUsuario)
                .NotEmpty().WithMessage("O campo email do usuario deve ser preenchido")
                .NotNull().WithMessage("O email do usuario não pode ser nulo");

            RuleFor(usuarioColaborador => usuarioColaborador.SenhaUsuario)
                .NotEmpty().WithMessage("O campo senha do usuario deve ser preenchido")
                .NotNull().WithMessage("A senha do usuario não pode ser nula");
        }
    }
}
