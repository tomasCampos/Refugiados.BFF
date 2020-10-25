using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using Repositorio.Repositorios.Interfaces;

namespace Repositorio.Repositorios
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly DataBaseConnector _db;

        public EmpresaRepositorio()
        {
            _db = new DataBaseConnector();
        }
        public async Task AtualizarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios)
        {                     
            await _db.ExecutarAsync(AppConstants.ATUALIZAR_EMPRESA, new { razao_social = razaoSocial,
                                                                            codigo_usuario = codigoUsuario,
                                                                            cnpj,
                                                                            nome_fantasia = nomeFantasia,
                                                                            data_fundacao = dataFundacao,
                                                                            numero_funcionarios = numeroFuncionarios });         
        }

        public async Task CadastrarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios, int? codigoEndereco)
        {
            await _db.ExecutarAsync(AppConstants.CADASTRAR_EMPRESA, new { codigo_usuario = codigoUsuario,
                                                                          razao_social = razaoSocial,
                                                                          cnpj, 
                                                                          nome_fantasia = nomeFantasia,
                                                                          data_fundacao = dataFundacao,
                                                                          numero_funcionarios = numeroFuncionarios,
                                                                          codigo_endereco = codigoEndereco});
        }

        public async Task<List<EmpresaDto>> ListarEmpresas()
        {
            var empresas = await _db.SelecionarAsync<EmpresaDto>(AppConstants.LISTAR_EMPRESAS_SQL);
            return empresas.ToList();
        }

        public async Task<EmpresaDto> ObterEmpresaPorCodigoUsuario(int codigoUsuario)
        {
            var response = await _db.SelecionarAsync<EmpresaDto>(AppConstants.OBTER_EMPRESA_POR_CODIGO_USUARIO, new { codigo_usuario = codigoUsuario });

            return response.FirstOrDefault();
        }
    }
}
