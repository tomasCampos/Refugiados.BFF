using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        public UsuarioRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public async Task<List<UsuarioDto>> ListarUsuarios() 
        {
            var result = await _dataBase.SelecionarAsync<UsuarioDto>(AppConstants.LISTAR_USUARIO_SQL);

            return result.ToList();
        }

        public async Task<List<UsuarioDto>> ListarUsuarios(int codigo) 
        {
            var result = await _dataBase.SelecionarAsync<UsuarioDto>(AppConstants.OBTER_USUARIO_POR_CODIGO_SQL, new { codigo_usuario = codigo });
            
            return result.ToList();
        }

        public async Task<List<UsuarioDto>> ListarUsuarios(string email)
        {
            var result = await _dataBase.SelecionarAsync<UsuarioDto>(AppConstants.OBTER_USUARIO_POR_EMAIL_SQL, new { email_usuario = email });
            
            return result.ToList();
        }

        public async Task CadastrarUsuario(string email, string senha, string telefoneUsuario, int? perfil) 
        {
            await _dataBase.ExecutarAsync(AppConstants.CADASTRAR_USUARIO, new { email_usuario = email, senha_usuario = senha, telefone_usuario = telefoneUsuario, perfil_usuario = perfil });
        }

        public async Task AtualizarUsuario(string email, string senha, bool entrevistado, string telefone, int codigo)
        {
            await _dataBase.ExecutarAsync(AppConstants.ATUALIZAR_USUARIO, new { email_usuario = email, senha_usuario = senha, telefone_usuario = telefone, entrevistado = entrevistado, codigo_usuario = codigo });            
        }

        public async Task DeletarUsuario(int codigoUsuario)
        {
            await _dataBase.ExecutarAsync(AppConstants.DELETAR_USUARIO, new { codigo_usuario = codigoUsuario });
        }
    }

    public interface IUsuarioRepositorio 
    {
        Task<List<UsuarioDto>> ListarUsuarios();
        Task<List<UsuarioDto>> ListarUsuarios(int codigo);
        Task<List<UsuarioDto>> ListarUsuarios(string email);
        Task CadastrarUsuario(string email, string senha, string telefoneUsuario, int? perfil);
        Task AtualizarUsuario(string email, string senha, bool entrevistado, string telefone, int codigo);
        Task DeletarUsuario(int codigoUsuario);
    }
}
