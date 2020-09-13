using System.Threading.Tasks;
using Refugiados.BFF.Models;

namespace Refugiados.BFF.Servicos.Interfaces
{
    public interface IEmpresaServico
    {
        Task<EmpresaModel> ObterEmpresaPorCodigoUsuario(int codigoUsuario);
        Task CadastrarEmpresa(string razaoSocial, int codigoUsuario);
        Task AtualizarEmpresa(string razaoSocial, int codigoUsuario);
    }
}
