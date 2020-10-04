using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos.Model
{
    public class DeletarUsuarioServiceModel
    {
        public SituacaoDelecaoUsuario SituacaoDelecao { get; set; }

        public DeletarUsuarioServiceModel(SituacaoDelecaoUsuario situacao)
        {
            SituacaoDelecao = situacao;
        }

        public enum SituacaoDelecaoUsuario
        {
            UsuarioInexistente,
            UsuarioDeletado,
            UsuarioAdmin
        };
    }
}
