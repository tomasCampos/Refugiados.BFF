using Refugiados.BFF.Models;
using Refugiados.BFF.Util;
using Repositorio.Dtos;
using Repositorio.Repositorios;
using System.Collections.Generic;


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

        public void CadastrarUsuario(string emailUsuario, string senhaUsuario)
        {            
            var senhaCifrada = CifrarSenhaUsuario(senhaUsuario);
            _usuarioRepositorio.CadastrarUsuario(emailUsuario, senhaCifrada);
        }

        public void AtualizarUsuario(string emailUsuario, string senhaUsuario, int codigoUsuario)
        {
            var senhaCifrada = string.Empty;
            if (!string.IsNullOrWhiteSpace(senhaUsuario))
                senhaCifrada = CifrarSenhaUsuario(senhaUsuario);

            _usuarioRepositorio.AtualizarUsuario(emailUsuario, senhaCifrada, codigoUsuario);
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
        void CadastrarUsuario(string emailUsuario, string senhaUsuario);
        void AtualizarUsuario(string emailUsuario, string senhaUsuario, int codigoUsuario);
    }
}
