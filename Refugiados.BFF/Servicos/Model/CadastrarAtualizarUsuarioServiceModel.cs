namespace Refugiados.BFF.Servicos.Model
{
    public class CadastrarAtualizarUsuarioServiceModel
    {
        public CadastrarAtualizarUsuarioServiceModel(SituacaoCadastroUsuario situacaoCadastro, int codigoUsuarioCadastrado)
        {
            SituacaoCadastro = situacaoCadastro;
            CodigoUsuarioCadastrado = codigoUsuarioCadastrado;
        }

        public enum SituacaoCadastroUsuario
        {
            UsuarioCadastrado,
            NomeDeUsuarioJaUtilizado            
        };

        public SituacaoCadastroUsuario SituacaoCadastro { get; private set; }
        public int CodigoUsuarioCadastrado { get; private set; }
    }
}
