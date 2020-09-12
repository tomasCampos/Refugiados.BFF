using System.Threading.Tasks;
using Repositorio.Dtos;

namespace Repositorio.Repositorios.Interfaces
{
    public interface IEmpresaRepositorio
    {
        public Task<EmpresaDto> ObterEmpresaPorId(string id);
        public Task CadastrarEmpresa(string razaoSocial, string codigoUsuario);
        public Task AtualizarEmpresa(string razaoSocial, string codigoUsuario);
    }
}