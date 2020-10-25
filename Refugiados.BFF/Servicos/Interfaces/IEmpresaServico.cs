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
        Task<int> CadastrarEmpresa(EmpresaModel empresa);
        Task AtualizarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios, List<int> codigosAreasTrabalho, EnderecoModel endereco);
    }
}
