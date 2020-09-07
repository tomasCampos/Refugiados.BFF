using Refugiados.BFF.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public UsuarioModel ObterUsuario()
        {
            var usuario = _usuarioRepositorio.ListarUsuarios().FirstOrDefault();

            var usuarioModel = new UsuarioModel
            {
                Codigo = usuario.codigo_usuario,
                Email = usuario.email_usuario,
                Senha = usuario.senha_usuario,
                DataCriacao = usuario.data_criacao,
                DataAlteracao = usuario.data_alteracao
            };

            return usuarioModel;
        }
    }

    public interface IUsuarioServico 
    {
        UsuarioModel ObterUsuario();
    }
}
