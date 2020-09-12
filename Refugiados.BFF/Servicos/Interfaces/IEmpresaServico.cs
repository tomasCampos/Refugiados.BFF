using System.Threading.Tasks;
using Refugiados.BFF.Models;

namespace Refugiados.BFF.Servicos.Interfaces
{
    public interface IEmpresaServico
    {
        Task<EmpresaModel> ObterEmpresaPorId(string id);
        Task CadastrarEmpresa(string razaoSocial, string codigoUsuario);
        Task AtualizarEmpresa(string razaoSocial, string codigoUsuario);
    }
}
