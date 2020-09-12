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
            var result = await _dataBase.Selecionar<UsuarioDto>(AppConstants.LISTAR_USUARIO_SQL);

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

        public async Task CadastrarUsuario(string email, string senha) 
        {
            await _dataBase.ExecutarAsync(AppConstants.CADASTRAR_USUARIO, new { email_usuario = email, senha_usuario = senha });
        }

        public async Task AtualizarUsuario(string email, string senha, int codigo)
        {
            if (string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(senha))
            {
                await _dataBase.ExecutarAsync(AppConstants.ATUALIZAR_SENHA_USUARIO, new { senha_usuario = senha, codigo_usuario = codigo });
            }
            else if (string.IsNullOrWhiteSpace(senha) && !string.IsNullOrWhiteSpace(email))
            {
                await _dataBase.ExecutarAsync(AppConstants.ATUALIZAR_EMAIL_USUARIO, new { email_usuario = email, codigo_usuario = codigo });
            }
            else if (!string.IsNullOrWhiteSpace(senha) && !string.IsNullOrWhiteSpace(email))
            {
                await _dataBase.ExecutarAsync(AppConstants.ATUALIZAR_USUARIO, new { email_usuario = email, senha_usuario = senha, codigo_usuario = codigo });
            }
        }
    }

    public interface IUsuarioRepositorio 
    {
        Task<List<UsuarioDto>> ListarUsuarios();
        Task<List<UsuarioDto>> ListarUsuarios(int codigo);
        Task<List<UsuarioDto>> ListarUsuarios(string email);
        Task CadastrarUsuario(string email, string senha);
        Task AtualizarUsuario(string email, string senha, int codigo);
    }
}
