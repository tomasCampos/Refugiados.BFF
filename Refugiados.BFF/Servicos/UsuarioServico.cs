using Microsoft.Extensions.Configuration;
using Refugiados.BFF.Models;
using Refugiados.BFF.Servicos.Model;
using Refugiados.BFF.Util;
using Repositorio.Dtos;
using Repositorio.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IConfiguration _configuration;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _configuration = configuration;
        }

        public async Task<List<UsuarioModel>> ListarUsuarios(int? codigoUsuario, string email)
        {
            var usuarios = new List<UsuarioDto>();

            if (!codigoUsuario.HasValue)
            {
                if (string.IsNullOrEmpty(email))
                    usuarios = await _usuarioRepositorio.ListarUsuarios();
                else
                    usuarios = await _usuarioRepositorio.ListarUsuarios(email);
            }
            else
            {
                usuarios = await _usuarioRepositorio.ListarUsuarios(codigoUsuario.Value);
            }

            var ListaDeUsuarios = new List<UsuarioModel>();
            
            foreach (var usuario in usuarios)
            {
                ListaDeUsuarios.Add(new UsuarioModel 
                {
                    Codigo = usuario.codigo_usuario,
                    Email = usuario.email_usuario,
                    Senha = usuario.senha_usuario,
                    DataCriacao = usuario.data_criacao,
                    DataAlteracao = usuario.data_alteracao,
                    PerfilUsuario = usuario.perfil_usuario
                });
            }

            return ListaDeUsuarios;
        }

        public async Task<CadastrarUsuarioServiceModel> CadastrarUsuario(string emailUsuario, string senhaUsuario, int? perfilUsuario)
        {
            var NomeDeUsuarioJaUtilizado = await ListarUsuarios(null, emailUsuario);
            if (NomeDeUsuarioJaUtilizado.Any())
                return new CadastrarUsuarioServiceModel(CadastrarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado, 0);

            var senhaCifrada = CifrarSenhaUsuario(senhaUsuario);            
            await _usuarioRepositorio.CadastrarUsuario(emailUsuario, senhaCifrada, perfilUsuario);
            var usuarioCadastrado = await ListarUsuarios(null, emailUsuario);

            return new CadastrarUsuarioServiceModel(CadastrarUsuarioServiceModel.SituacaoCadastroUsuario.UsuarioCadastrado, usuarioCadastrado.FirstOrDefault().Codigo);
        }

        public async Task AtualizarUsuario(string emailUsuario, string senhaUsuario, int codigoUsuario)
        {
            var senhaCifrada = string.Empty;
            if (!string.IsNullOrWhiteSpace(senhaUsuario))
                senhaCifrada = CifrarSenhaUsuario(senhaUsuario);

            await _usuarioRepositorio.AtualizarUsuario(emailUsuario, senhaCifrada, codigoUsuario);
        }

        public async Task<AutenticarUsuarioServiceModel> AutenticarUsuario(string emailUsuario, string senhaUsuario)
        {
            var usuarios = await ListarUsuarios(null, emailUsuario);

            if (usuarios.FirstOrDefault() == null)
            {
                return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.NomeDeUsuarioInvalido, 0);
            }

            var senhaCifrada = CifrarSenhaUsuario(senhaUsuario);

            if (!string.Equals(usuarios.FirstOrDefault().Senha, senhaCifrada))
            {
                return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.SenhaInvalida, 0);
            }

            return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.UsuarioAutenticado, usuarios.FirstOrDefault().Codigo);
        }

        #region METODOS PRIVADOS

        private string CifrarSenhaUsuario(string senha)
        {
            var cifraSenha = _configuration.GetValue<string>("AppConfig:ChaveCifra");
            var senhaCifrada = AES.Encrypt(senha, cifraSenha);
            
            return senhaCifrada;
        }
            
        #endregion
    }

    public interface IUsuarioServico 
    {
        Task<List<UsuarioModel>> ListarUsuarios(int? codigoUsuario, string email);
        Task<CadastrarUsuarioServiceModel> CadastrarUsuario(string emailUsuario, string senhaUsuario, int? perfilUsuario);
        Task AtualizarUsuario(string emailUsuario, string senhaUsuario, int codigoUsuario);
        Task<AutenticarUsuarioServiceModel> AutenticarUsuario(string emailUsuario, string senhaUsuario);
    }
}
