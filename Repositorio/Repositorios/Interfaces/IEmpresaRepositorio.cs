using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repositorio.Dtos;

namespace Repositorio.Repositorios.Interfaces
{
    public interface IEmpresaRepositorio
    {
        public Task<EmpresaDto> ObterEmpresaPorCodigoUsuario(int codigoUsuario);
        public Task<List<EmpresaDto>> ListarEmpresas(string nomeFantasia, string cidade, int? codigoAreaTrabalho);
        public Task CadastrarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios, int codigoEndereco);
        public Task AtualizarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios);
    }
}