using System.Collections.Generic;
using System.Threading.Tasks;
using Repositorio.Dtos;

namespace Repositorio.Repositorios.Interfaces
{
    public interface IEmpresaRepositorio
    {
        public Task<EmpresaDto> ObterEmpresaPorCodigoUsuario(int codigoUsuario);
        public Task<List<EmpresaDto>> ListarEmpresas();
        public Task CadastrarEmpresa(string razaoSocial, int codigoUsuario);
        public Task AtualizarEmpresa(string razaoSocial, int codigoUsuario);
    }
}