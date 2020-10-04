using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refugiados.BFF.Models;

namespace Refugiados.BFF.Servicos.Interfaces
{
    public interface IEmpresaServico
    {
        Task<EmpresaModel> ObterEmpresaPorCodigoUsuario(int codigoUsuario);
        Task<List<EmpresaModel>> ListarEmpresas();
        Task CadastrarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios);
        Task AtualizarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios);
    }
}
