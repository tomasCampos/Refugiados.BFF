﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;

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
                                                                            numero_funcionarios = numeroFuncionarios});         
        }

        public async Task CadastrarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios, int codigoEndereco)
        {
            await _db.ExecutarAsync(AppConstants.CADASTRAR_EMPRESA, new 
            { 
                codigo_usuario = codigoUsuario,
                razao_social = razaoSocial,
                cnpj, 
                nome_fantasia = nomeFantasia,
                data_fundacao = dataFundacao,
                numero_funcionarios = numeroFuncionarios,
                codigo_endereco = codigoEndereco
            });
        }

        public async Task<List<EmpresaDto>> ListarEmpresas(string nomeFantasia, string cidade, int? codigoAreaTrabalho, bool? entrevistado)
        {
            var filtroNomeFantasia = string.Empty;
            var filtroCidade = string.Empty;
            var filtroAreasTrabalho = string.Empty;
            var filtroEntrevistado = string.Empty;

            var joinAreaTrabalho = string.Empty;

            if (!string.IsNullOrEmpty(nomeFantasia))
                filtroNomeFantasia = $"AND e.nome_fantasia LIKE '%{nomeFantasia}%'";

            if (!string.IsNullOrEmpty(cidade))
            {
                filtroCidade = $"AND en.cidade_endereco LIKE '%{cidade}%'";
            }

            if (codigoAreaTrabalho.HasValue)
            {
                joinAreaTrabalho = "INNER JOIN empresa_area_trabalho AS eat ON eat.codigo_empresa = e.codigo_empresa";
                filtroAreasTrabalho = $"AND eat.codigo_area_trabalho = {codigoAreaTrabalho.Value}";
            }

            if (entrevistado.HasValue)
            {
                filtroEntrevistado = $"AND u.entrevistado = {entrevistado.Value}";
            }

            var query = string.Format(AppConstants.LISTAR_EMPRESAS_SQL, joinAreaTrabalho, filtroCidade, filtroAreasTrabalho, filtroNomeFantasia, filtroEntrevistado);
            var empresas = await _db.SelecionarAsync<EmpresaDto>(query);
            return empresas.ToList();
        }

        public async Task<EmpresaDto> ObterEmpresaPorCodigoUsuario(int codigoUsuario)
        {
            var response = await _db.SelecionarAsync<EmpresaDto>(AppConstants.OBTER_EMPRESA_POR_CODIGO_USUARIO, new { codigo_usuario = codigoUsuario });

            return response.FirstOrDefault();
        }
    }

    public interface IEmpresaRepositorio
    {
        public Task<EmpresaDto> ObterEmpresaPorCodigoUsuario(int codigoUsuario);
        public Task<List<EmpresaDto>> ListarEmpresas(string nomeFantasia, string cidade, int? codigoAreaTrabalho, bool? entrevistado);
        public Task CadastrarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios, int codigoEndereco);
        public Task AtualizarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios);
    }
}
