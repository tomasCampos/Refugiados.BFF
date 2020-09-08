using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos.Model
{
    public class AutenticarUsuarioServiceModel
    {
        public AutenticarUsuarioServiceModel(Situacao situacao, int codigoUsuario)
        {
            SituacaoAutenticacao = situacao;
            CodigoUsuario = codigoUsuario;
        }

        public enum Situacao
        {
            UsuarioAutenticado,
            NomeDeUsuarioInvalido,
            SenhaInvalida,
        };

        public Situacao SituacaoAutenticacao { get; private set; }

        public int CodigoUsuario { get; private set; }
    }
}
