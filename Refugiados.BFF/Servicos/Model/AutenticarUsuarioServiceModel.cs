using Microsoft.Net.Http.Headers;

namespace Refugiados.BFF.Servicos.Model
{
    public class AutenticarUsuarioServiceModel
    {
        public AutenticarUsuarioServiceModel(SituacaoAutenticacaoUsuario situacao, int codigoUsuario, int? perfilUsuario)
        {
            SituacaoAutenticacao = situacao;
            CodigoUsuario = codigoUsuario;
            PerfilUsuario = perfilUsuario;
        }

        public enum SituacaoAutenticacaoUsuario
        {
            UsuarioAutenticado,
            NomeDeUsuarioInvalido,
            SenhaInvalida,
        };

        public SituacaoAutenticacaoUsuario SituacaoAutenticacao { get; private set; }

        public int CodigoUsuario { get; private set; }

        public int? PerfilUsuario { get;  private set; }
    }
}
