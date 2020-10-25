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
        private readonly IAreaTrabalhoServico _areaTrabalhoServico;
        private readonly IEnderecoServico _enderecoServico;

        public EmpresaServico(IEmpresaRepositorio empresaRepositorio, IAreaTrabalhoServico areaTrabalhoServico, IEnderecoServico enderecoServico)
        {
            _empresaRepositorio = empresaRepositorio;
            _areaTrabalhoServico = areaTrabalhoServico;
            _enderecoServico = enderecoServico;
        }

        public async Task<int> CadastrarEmpresa(EmpresaModel empresa)
        {
            var codigoEnderecoCadastrado = await _enderecoServico.CadastrarEndereco(empresa.Endereco);            

            await _empresaRepositorio.CadastrarEmpresa(empresa.RazaoSocial, empresa.CodigoUsuario, empresa.CNPJ, empresa.NomeFantasia, empresa.DataFundacao, empresa.NumeroFuncionarios, codigoEnderecoCadastrado);

            var empresaCadastrada = await ObterEmpresaPorCodigoUsuario(empresa.CodigoUsuario);

            if(empresa.AreasTrabalho != null)
                await _areaTrabalhoServico.CadastrarAtualizarAreaTrabalhoEmpresa(empresaCadastrada.CodigoEmpresa, empresa.AreasTrabalho);

            return empresaCadastrada.CodigoEmpresa;
        }

        public async Task AtualizarEmpresa(string razaoSocial, int codigoUsuario, string cnpj, string nomeFantasia, DateTime? dataFundacao, int? numeroFuncionarios, 
            List<int> codigosAreasTrabalho, EnderecoModel endereco)
        {
            var empresa = await ObterEmpresaPorCodigoUsuario(codigoUsuario);

            if (empresa == null)
                return;

            if (endereco != null)
            {
                await _enderecoServico.AtualizarEndereco(empresa.Endereco.CodigoEndereco, endereco.CidadeEndereco, endereco.BairroEndereco, endereco.RuaEndereco, endereco.ComplementoEndereco,
                    endereco.ComplementoEndereco, endereco.CepEndereco, endereco.EstadoEndereco);
            }

            empresa.RazaoSocial = string.IsNullOrEmpty(razaoSocial) ? empresa.RazaoSocial : razaoSocial;
            empresa.CNPJ = string.IsNullOrEmpty(cnpj) ? empresa.CNPJ : cnpj;
            empresa.NomeFantasia = string.IsNullOrEmpty(nomeFantasia) ? empresa.NomeFantasia : nomeFantasia;
            empresa.DataFundacao = dataFundacao.HasValue ? dataFundacao : empresa.DataFundacao;
            empresa.NumeroFuncionarios = numeroFuncionarios.HasValue ? numeroFuncionarios : empresa.NumeroFuncionarios;    
            await _empresaRepositorio.AtualizarEmpresa(empresa.RazaoSocial, codigoUsuario, empresa.CNPJ, empresa.NomeFantasia, empresa.DataFundacao, empresa.NumeroFuncionarios);

            if (codigosAreasTrabalho != null)
            {
                var listaAreasTrabalho = new List<AreaTrabalhoModel>();
                foreach (var codigo in codigosAreasTrabalho)
                {
                    listaAreasTrabalho.Add(new AreaTrabalhoModel { CodigoAreaTrabalho = codigo });
                }

                await _areaTrabalhoServico.CadastrarAtualizarAreaTrabalhoEmpresa(empresa.CodigoEmpresa, listaAreasTrabalho);
            }           
        }

        public async Task<EmpresaModel> ObterEmpresaPorCodigoUsuario(int codigoUsuario)
        {
            var empresa = await _empresaRepositorio.ObterEmpresaPorCodigoUsuario(codigoUsuario);

            if (empresa == null)
                return null;

            var areasTrabalhoEmpresa = await _areaTrabalhoServico.ListarAreasTrabalhoEmpresa(empresa.codigo_empresa);

            var resultado =  new EmpresaModel
            {
                CodigoEmpresa = empresa.codigo_empresa,
                CodigoUsuario = empresa.codigo_usuario,
                RazaoSocial = empresa.razao_social,
                CNPJ = empresa.cnpj,
                NomeFantasia = empresa.nome_fantasia,
                DataFundacao = empresa.data_fundacao,
                NumeroFuncionarios = empresa.numero_funcionarios,
                EmailUsuario = empresa.email_usuario,
                DataCriacao = empresa.data_criacao,
                DataAlteracao = empresa.data_alteracao,
                Entrevistado = empresa.entrevistado,
                TelefoneUsuario = empresa.telefone_usuario,
                AreasTrabalho = areasTrabalhoEmpresa.ToList(),
                Endereco = empresa.codigo_endereco.HasValue ? new EnderecoModel { CodigoEndereco = empresa.codigo_endereco.Value } : null
            };

            if (resultado.Endereco != null)
            {
                var endereco = await _enderecoServico.ObterEndereco(resultado.Endereco.CodigoEndereco);
                resultado.Endereco = endereco;
            }

            return resultado;
        }

        public async Task<List<EmpresaModel>> ListarEmpresas()
        {
            var lista = await _empresaRepositorio.ListarEmpresas();

            var empresas = lista.Select(empresa => new EmpresaModel
            {
                CodigoEmpresa = empresa.codigo_empresa,
                CodigoUsuario = empresa.codigo_usuario,
                RazaoSocial = empresa.razao_social,
                CNPJ = empresa.cnpj,
                NomeFantasia = empresa.nome_fantasia,
                DataFundacao = empresa.data_fundacao,
                NumeroFuncionarios = empresa.numero_funcionarios,
                EmailUsuario = empresa.email_usuario,
                DataCriacao = empresa.data_criacao,
                DataAlteracao = empresa.data_alteracao,
                Entrevistado = empresa.entrevistado,
                TelefoneUsuario = empresa.telefone_usuario,
                Endereco = empresa.codigo_endereco.HasValue ? new EnderecoModel { CodigoEndereco = empresa.codigo_endereco.Value } : null
            }).ToList();

            foreach (var empresa in empresas)
            {
                var areasTrabalhoEmpresa = await _areaTrabalhoServico.ListarAreasTrabalhoEmpresa(empresa.CodigoEmpresa);
                empresa.AreasTrabalho = areasTrabalhoEmpresa.ToList();

                if (empresa.Endereco != null)
                {
                    var endereco = await _enderecoServico.ObterEndereco(empresa.Endereco.CodigoEndereco);
                    empresa.Endereco = endereco;
                }
            }

            return empresas;
        }
    }
}

