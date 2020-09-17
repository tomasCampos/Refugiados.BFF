using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos.Model
{
    public class CadastrarUsuarioServiceModel
    {
        public CadastrarUsuarioServiceModel(SituacaoCadastroUsuario situacaoCadastro, int codigoUsuarioCadastrado)
        {
            SituacaoCadastro = situacaoCadastro;
            CodigoUsuarioCadastrado = codigoUsuarioCadastrado;
        }

        public enum SituacaoCadastroUsuario
        {
            UsuarioCadastrado,
            NomeDeUsuarioJaUtilizado
        };

        public SituacaoCadastroUsuario SituacaoCadastro { get; set; }
        public int CodigoUsuarioCadastrado { get; set; }
    }
}
