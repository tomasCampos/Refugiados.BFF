using Microsoft.Extensions.Configuration;
using Refugiados.BFF.Models;
using Refugiados.BFF.Servicos.Interfaces;
using Refugiados.BFF.Servicos.Model;
using Refugiados.BFF.Util;
using Repositorio.Dtos;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Repositorio.CrossCutting.AppConstants;

namespace Refugiados.BFF.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IConfiguration _configuration;
        private readonly IColaboradorSerivico _colaboradorServico;
        private readonly IEmpresaServico _empresaServico;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, IConfiguration configuration, IColaboradorSerivico colaboradorServico, IEmpresaServico empresaServico)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _configuration = configuration;
            _colaboradorServico = colaboradorServico;
            _empresaServico = empresaServico;
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
                    CodigoUsuario = usuario.codigo_usuario,
                    EmailUsuario = usuario.email_usuario,
                    SenhaUsuario = usuario.senha_usuario,
                    DataCriacao = usuario.data_criacao,
                    DataAlteracao = usuario.data_alteracao,
                    PerfilUsuario = usuario.perfil_usuario,
                    Entrevistado = usuario.entrevistado
                });
            }

            return ListaDeUsuarios;
        }

        public async Task<CadastrarAtualizarUsuarioServiceModel> CadastrarUsuario(string emailUsuario, string senhaUsuario, int? perfilUsuario = null)
        {
            var NomeDeUsuarioJaUtilizado = await ListarUsuarios(null, emailUsuario);
            if (NomeDeUsuarioJaUtilizado.Any())
                return new CadastrarAtualizarUsuarioServiceModel(CadastrarAtualizarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado, 0);

            var senhaCifrada = CifrarSenhaUsuario(senhaUsuario);            
            await _usuarioRepositorio.CadastrarUsuario(emailUsuario, senhaCifrada, perfilUsuario);
            var usuarioCadastrado = await ListarUsuarios(null, emailUsuario);

            return new CadastrarAtualizarUsuarioServiceModel(CadastrarAtualizarUsuarioServiceModel.SituacaoCadastroUsuario.UsuarioCadastrado, usuarioCadastrado.FirstOrDefault().CodigoUsuario);
        }

        public async Task<CadastrarAtualizarUsuarioServiceModel> CadastrarUsuarioColaborador(string emailUsuario, string senhaUsuario, ColaboradorModel colaborador) 
        {
            var resultadoCadastroUsuario = await CadastrarUsuario(emailUsuario, senhaUsuario, (int)PerfilUsuario.Colaborador);

            if(resultadoCadastroUsuario.SituacaoCadastro == CadastrarAtualizarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado)
                    return resultadoCadastroUsuario;

            colaborador.CodigoUsuario = resultadoCadastroUsuario.CodigoUsuarioCadastrado;
            await _colaboradorServico.CadastrarColaborador(colaborador);

            return resultadoCadastroUsuario;
        }

        public async Task<CadastrarAtualizarUsuarioServiceModel> CadastrarUsuarioEmpresa(string emailUsuario, string senhaUsuario, string razaoSocial, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios)
        {
            var resultadoCadastroUsuario = await CadastrarUsuario(emailUsuario, senhaUsuario, (int)PerfilUsuario.Empresa);

            if (resultadoCadastroUsuario.SituacaoCadastro == CadastrarAtualizarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado)
                return resultadoCadastroUsuario;

            await _empresaServico.CadastrarEmpresa(razaoSocial, resultadoCadastroUsuario.CodigoUsuarioCadastrado, cnpj, nomeFantasia, dataFundacao, numeroFuncionarios);

            return resultadoCadastroUsuario;
        }

        public async Task<CadastrarAtualizarUsuarioServiceModel> AtualizarUsuario(string emailUsuario, string senhaUsuario, bool? entrevistado, int codigoUsuario)
        {
            if(!string.IsNullOrWhiteSpace(emailUsuario))
            {
                var NomeDeUsuarioJaUtilizado = await ListarUsuarios(null, emailUsuario);
                if (NomeDeUsuarioJaUtilizado.Any())
                    return new CadastrarAtualizarUsuarioServiceModel(CadastrarAtualizarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado, 0);
            }

            var senhaCifrada = string.Empty;
            if (!string.IsNullOrWhiteSpace(senhaUsuario))
                senhaCifrada = CifrarSenhaUsuario(senhaUsuario);

            var usuarios = await ListarUsuarios(codigoUsuario, null);
            var usuarioAtual = usuarios.FirstOrDefault();

            if (usuarioAtual != null)
            {
                usuarioAtual.EmailUsuario = !string.IsNullOrEmpty(emailUsuario) ? emailUsuario : usuarioAtual.EmailUsuario;
                usuarioAtual.SenhaUsuario = !string.IsNullOrEmpty(senhaCifrada) ? senhaCifrada : usuarioAtual.SenhaUsuario;
                usuarioAtual.Entrevistado = entrevistado.HasValue ? entrevistado.Value : usuarioAtual.Entrevistado;
                await _usuarioRepositorio.AtualizarUsuario(usuarioAtual.EmailUsuario, usuarioAtual.SenhaUsuario, usuarioAtual.Entrevistado, codigoUsuario);                         
            }

            return new CadastrarAtualizarUsuarioServiceModel(CadastrarAtualizarUsuarioServiceModel.SituacaoCadastroUsuario.UsuarioCadastrado, codigoUsuario);
        }

        public async Task<AutenticarUsuarioServiceModel> AutenticarUsuario(string emailUsuario, string senhaUsuario)
        {
            var usuarios = await ListarUsuarios(null, emailUsuario);
            var usuario = usuarios.FirstOrDefault();

            if (usuario == null)            
                return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.NomeDeUsuarioInvalido, 0, 0);            

            if (usuario.PerfilUsuario.HasValue && usuario.Entrevistado == false)
                return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.UsuarioAindaNaoEntrevistado, 0, 0);

            var senhaCifrada = CifrarSenhaUsuario(senhaUsuario);

            if (!string.Equals(usuario.SenhaUsuario, senhaCifrada))            
                return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.SenhaInvalida, 0, 0);            

            return new AutenticarUsuarioServiceModel(AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.UsuarioAutenticado, usuario.CodigoUsuario, usuario.PerfilUsuario);
        }

        public async Task<DeletarUsuarioServiceModel> DeletarUsuario(int codigoUsuario)
        {
            var listaUsuarios = await ListarUsuarios(codigoUsuario, null);
            var usuario = listaUsuarios.FirstOrDefault();

            if (usuario == null)
                return new DeletarUsuarioServiceModel(DeletarUsuarioServiceModel.SituacaoDelecaoUsuario.UsuarioInexistente);

            if (usuario.PerfilUsuario == null)
                return new DeletarUsuarioServiceModel(DeletarUsuarioServiceModel.SituacaoDelecaoUsuario.UsuarioAdmin);

            await _usuarioRepositorio.DeletarUsuario(codigoUsuario);
            return new DeletarUsuarioServiceModel(DeletarUsuarioServiceModel.SituacaoDelecaoUsuario.UsuarioDeletado);
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
        Task<CadastrarAtualizarUsuarioServiceModel> CadastrarUsuario(string emailUsuario, string senhaUsuario, int? perfilUsuario = null);
        Task<CadastrarAtualizarUsuarioServiceModel> CadastrarUsuarioColaborador(string emailUsuario, string senhaUsuario, ColaboradorModel colaborador);
        Task<CadastrarAtualizarUsuarioServiceModel> CadastrarUsuarioEmpresa(string emailUsuario, string senhaUsuario, string razaoSocial, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios);
        Task<CadastrarAtualizarUsuarioServiceModel> AtualizarUsuario(string emailUsuario, string senhaUsuario, bool? entrevistado, int codigoUsuario);
        Task<AutenticarUsuarioServiceModel> AutenticarUsuario(string emailUsuario, string senhaUsuario);
        Task<DeletarUsuarioServiceModel> DeletarUsuario(int codigoUsuario);
    }
}
