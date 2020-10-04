using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refugiados.BFF.Models;
using Refugiados.BFF.Servicos.Interfaces;
using Repositorio.Repositorios.Interfaces;

namespace Refugiados.BFF.Servicos
{
    public class EmpresaServico : IEmpresaServico
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EmpresaServico(IEmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        public async Task CadastrarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios)
        {
            await _empresaRepositorio.CadastrarEmpresa(razaoSocial, codigoUsuario, cnpj, nomeFantasia, dataFundacao, numeroFuncionarios);
        }

        public async Task AtualizarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios)
        {
            var empresa = await ObterEmpresaPorCodigoUsuario(codigoUsuario);

            if (empresa == null)
                return;

            empresa.RazaoSocial = string.IsNullOrEmpty(razaoSocial) ? empresa.RazaoSocial : razaoSocial;
            empresa.CNPJ = string.IsNullOrEmpty(cnpj) ? empresa.CNPJ : cnpj;
            empresa.NomeFantasia = string.IsNullOrEmpty(nomeFantasia) ? empresa.NomeFantasia : nomeFantasia;
            empresa.DataFundacao = dataFundacao.HasValue ? dataFundacao : empresa.DataFundacao;
            empresa.NumeroFuncionarios = numeroFuncionarios.HasValue ? numeroFuncionarios : empresa.NumeroFuncionarios;

            await _empresaRepositorio.AtualizarEmpresa(empresa.RazaoSocial, codigoUsuario, empresa.CNPJ, empresa.NomeFantasia, empresa.DataFundacao, empresa.NumeroFuncionarios);
        }

        public async Task<EmpresaModel> ObterEmpresaPorCodigoUsuario(int codigoUsuario)
        {
            var empresa = await _empresaRepositorio.ObterEmpresaPorCodigoUsuario(codigoUsuario);

            if (empresa != null)
            {
                return new EmpresaModel
                {
                    CodigoEmpresa = empresa.codigo_empresa,
                    CodigoUsuario = empresa.codigo_usuario,
                    RazaoSocial = empresa.razao_social,
                    CNPJ = empresa.cnpj,
                    NomeFantasia = empresa.nome_fantasia,
                    DataFundacao = empresa.data_fundacao,
                    NumeroFuncionarios = empresa.numero_funcionarios,
                    EmailContato = empresa.email_usuario,
                    DataCriacao = empresa.data_criacao,
                    DataAlteracao = empresa.data_alteracao,
                    Entrevistado = empresa.entrevistado
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<List<EmpresaModel>> ListarEmpresas()
        {
            var lista = await _empresaRepositorio.ListarEmpresas();

            var colaboradores = lista.Select(empresa => new EmpresaModel
            {
                CodigoEmpresa = empresa.codigo_empresa,
                CodigoUsuario = empresa.codigo_usuario,
                RazaoSocial = empresa.razao_social,
                CNPJ = empresa.cnpj,
                NomeFantasia = empresa.nome_fantasia,
                DataFundacao = empresa.data_fundacao,
                NumeroFuncionarios = empresa.numero_funcionarios,
                EmailContato = empresa.email_usuario,
                DataCriacao = empresa.data_criacao,
                DataAlteracao = empresa.data_alteracao,
                Entrevistado = empresa.entrevistado
            }).ToList();

            return colaboradores;
        }
    }
}

