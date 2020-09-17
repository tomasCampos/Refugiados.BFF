namespace Refugiados.BFF.Servicos.Model
{
    public class AutenticarUsuarioServiceModel
    {
        public AutenticarUsuarioServiceModel(SituacaoAutenticacaoUsuario situacao, int codigoUsuario)
        {
            SituacaoAutenticacao = situacao;
            CodigoUsuario = codigoUsuario;
        }

        public enum SituacaoAutenticacaoUsuario
        {
            UsuarioAutenticado,
            NomeDeUsuarioInvalido,
            SenhaInvalida,
        };

        public SituacaoAutenticacaoUsuario SituacaoAutenticacao { get; private set; }

        public int CodigoUsuario { get; private set; }
    }
}
