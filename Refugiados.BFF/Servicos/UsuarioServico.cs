using Refugiados.BFF.Models;
using Refugiados.BFF.Servicos.Model;
using Refugiados.BFF.Util;
using Repositorio.Dtos;
using Repositorio.Repositorios;
using System.Collections.Generic;
using System.Linq;

namespace Refugiados.BFF.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private const string CHAVE_CIFRA_SENHA = "1e64fdce-e561-4f3d-bb78-0d7c8c86d14b";
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public List<UsuarioModel> ListarUsuarios(int? codigoUsuario, string email)
        {
            var usuarios = new List<UsuarioDto>();
            if (!codigoUsuario.HasValue)
            {
                if (string.IsNullOrEmpty(email))
                    usuarios = _usuarioRepositorio.ListarUsuarios();
                else
                    usuarios = _usuarioRepositorio.ListarUsuarios(email);
            }               
            else
                usuarios = _usuarioRepositorio.ListarUsuarios(codigoUsuario.Value);

            var ListaDeUsuarios = new List<UsuarioModel>();
            foreach (var usuario in usuarios)
            {
                ListaDeUsuarios.Add(new UsuarioModel 
                {
                    Codigo = usuario.codigo_usuario,
                    Email = usuario.email_usuario,
                    Senha = usuario.senha_usuario,
                    DataCriacao = usuario.data_criacao,
                    DataAlteracao = usuario.data_alteracao
                });
            }

            return ListaDeUsuarios;
        }

        public int CadastrarUsuario(string emailUsuario, string senhaUsuario)
        {            
            var senhaCifrada = CifrarSenhaUsuario(senhaUsuario);
            _usuarioRepositorio.CadastrarUsuario(emailUsuario, senhaCifrada);

            var usuarioCadastrado = ListarUsuarios(null, emailUsuario).First();

            return usuarioCadastrado.Codigo;
        }

        public void AtualizarUsuario(string emailUsuario, string senhaUsuario, int codigoUsuario)
        {
            var senhaCifrada = string.Empty;
            if (!string.IsNullOrWhiteSpace(senhaUsuario))
                senhaCifrada = CifrarSenhaUsuario(senhaUsuario);

            _usuarioRepositorio.AtualizarUsuario(emailUsuario, senhaCifrada, codigoUsuario);
        }

        public AutenticarUsuarioServiceModel AutenticarUsuario(string emailUsuario, string senhaUsuario)
        {
            var usuario = ListarUsuarios(null, emailUsuario).FirstOrDefault();

            if (usuario == null)
                return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.Situacao.NomeDeUsuarioInvalido, 0);

            var senhaCifrada = CifrarSenhaUsuario(senhaUsuario);

            if (!string.Equals(usuario.Senha, senhaCifrada))
                return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.Situacao.SenhaInvalida, 0);

            return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.Situacao.UsuarioAutenticado, usuario.Codigo);
        }

        #region METODOS PRIVADOS

        private string CifrarSenhaUsuario(string senha)
        {            
            var senhaCifrada = AES.Encrypt(senha, CHAVE_CIFRA_SENHA);
            return senhaCifrada;
        }
            
        #endregion
    }

    public interface IUsuarioServico 
    {
        List<UsuarioModel> ListarUsuarios(int? codigoUsuario, string email);
        int CadastrarUsuario(string emailUsuario, string senhaUsuario);
        void AtualizarUsuario(string emailUsuario, string senhaUsuario, int codigoUsuario);
        AutenticarUsuarioServiceModel AutenticarUsuario(string emailUsuario, string senhaUsuario);
    }
}
