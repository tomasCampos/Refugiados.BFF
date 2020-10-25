using Refugiados.BFF.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos
{
    public class ColaboradorServico : IColaboradorSerivico
    {
        private readonly IColaboradorRepositorio _colaboradorRepositorio;
        private readonly IIdiomaServico _idiomaServico;
        private readonly IAreaTrabalhoServico _areaTrabalhoServico;
        private readonly IEnderecoServico _enderecoServico;

        public ColaboradorServico(IColaboradorRepositorio colaboradorRepositorio, IIdiomaServico idiomaServico, IAreaTrabalhoServico areaTrabalhoServico, IEnderecoServico enderecoServico)
        {
            _colaboradorRepositorio = colaboradorRepositorio;
            _idiomaServico = idiomaServico;
            _areaTrabalhoServico = areaTrabalhoServico;
            _enderecoServico = enderecoServico;
        }

        public async Task AtualizarColaborador(string nome, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade, 
            int codigoUsuario, List<int> codigosIdiomas, List<int> codigosAreasTrabalho, EnderecoModel endereco)
        {
            var colaborador = await ObterColaboradorPorCodigoUsuario(codigoUsuario);

            if (colaborador == null)
                return;

            if (endereco != null)
            {
                await _enderecoServico.AtualizarEndereco(colaborador.Endereco.CodigoEndereco, endereco.CidadeEndereco, endereco.BairroEndereco, endereco.RuaEndereco, endereco.ComplementoEndereco,
                    endereco.ComplementoEndereco, endereco.CepEndereco, endereco.EstadoEndereco);
            }

            colaborador.NomeColaborador = string.IsNullOrEmpty(nome) ? colaborador.NomeColaborador : nome;
            colaborador.Nacionalidade = string.IsNullOrEmpty(nacionalidade) ? colaborador.Nacionalidade : nacionalidade;
            colaborador.DataNascimento = !dataNascimento.HasValue ? colaborador.DataNascimento : dataNascimento;
            colaborador.DataChegadaBrasil = !dataChegadaBrasil.HasValue ? colaborador.DataChegadaBrasil : dataChegadaBrasil;            
            colaborador.AreaFormacao = string.IsNullOrEmpty(areaFormacao) ? colaborador.AreaFormacao : areaFormacao;
            colaborador.Escolaridade = string.IsNullOrEmpty(escolaridade) ? colaborador.Escolaridade : escolaridade;

            await _colaboradorRepositorio.AtualizarColaborador(colaborador.NomeColaborador, colaborador.CodigoUsuario, colaborador.Nacionalidade, colaborador.DataNascimento, 
                colaborador.DataChegadaBrasil, colaborador.AreaFormacao, colaborador.Escolaridade);

            if (codigosIdiomas != null)
            {
                var listaIdiomas = new List<IdiomaModel>();
                foreach (var codigo in codigosIdiomas)
                {
                    listaIdiomas.Add(new IdiomaModel { CodigoIdioma = codigo });
                }

                await _idiomaServico.CadastrarAtualizarIdiomaColaborador(colaborador.CodigoColaborador, listaIdiomas);
            }

            if (codigosAreasTrabalho != null)
            {
                var listaAreasTrabalho = new List<AreaTrabalhoModel>();
                foreach (var codigo in codigosAreasTrabalho)
                {
                    listaAreasTrabalho.Add(new AreaTrabalhoModel { CodigoAreaTrabalho = codigo });
                }

                await _areaTrabalhoServico.CadastrarAtualizarAreaTrabalhoColaborador(colaborador.CodigoColaborador, listaAreasTrabalho);
            }
        }

        public async Task<int> CadastrarColaborador(ColaboradorModel colaborador)
        {
            var codigoEnderecoCadastrado = await _enderecoServico.CadastrarEndereco(colaborador.Endereco);

            await _colaboradorRepositorio.CadastrarColaborador(colaborador.NomeColaborador, colaborador.CodigoUsuario, colaborador.Nacionalidade, colaborador.DataNascimento,
                colaborador.DataChegadaBrasil, colaborador.AreaFormacao, colaborador.Escolaridade, codigoEnderecoCadastrado);

            var colaboradorCadastrado = await ObterColaboradorPorCodigoUsuario(colaborador.CodigoUsuario);

            if(colaborador.Idiomas != null && colaborador.Idiomas.Any())
                await _idiomaServico.CadastrarAtualizarIdiomaColaborador(colaboradorCadastrado.CodigoColaborador, colaborador.Idiomas);

            if(colaborador.AreasTrabalho != null && colaborador.AreasTrabalho.Any())
                await _areaTrabalhoServico.CadastrarAtualizarAreaTrabalhoColaborador(colaboradorCadastrado.CodigoColaborador, colaborador.AreasTrabalho);

            return colaboradorCadastrado.CodigoColaborador;
        }

        public async Task<List<ColaboradorModel>> ListarColaboradores()
        {
            var lista = await _colaboradorRepositorio.ListarColaboradores();

            var colaboradores = lista.Select(colab => new ColaboradorModel
            {
                DataAlteracao = colab.data_alteracao,
                DataCriacao = colab.data_criacao,
                CodigoColaborador = colab.codigo_colaborador,
                CodigoUsuario = colab.codigo_usuario,
                NomeColaborador = colab.nome_colaborador,
                EmailUsuario = colab.email_usuario,
                Nacionalidade = colab.nacionalidade,
                DataChegadaBrasil = colab.data_chegada_brasil,
                DataNascimento = colab.data_nascimento,
                Escolaridade = colab.escolaridade,
                AreaFormacao = colab.area_formacao,
                Entrevistado = colab.entrevistado,
                TelefoneUsuario = colab.telefone_usuario,
                Endereco = new EnderecoModel { CodigoEndereco = colab.codigo_endereco }
            }).ToList();

            foreach (var colaborador in colaboradores)
            {
                var idiomasColaborador = await _idiomaServico.ListarIdiomaColaborador(colaborador.CodigoColaborador);
                colaborador.Idiomas = idiomasColaborador.ToList();

                var areasTrabalhoColaborador = await _areaTrabalhoServico.ListarAreasTrabalhoColaborador(colaborador.CodigoColaborador);
                colaborador.AreasTrabalho = areasTrabalhoColaborador.ToList();

                var endereco = await _enderecoServico.ObterEndereco(colaborador.Endereco.CodigoEndereco);
                colaborador.Endereco = endereco;
            }

            return colaboradores;
        }

        public async Task<ColaboradorModel> ObterColaboradorPorCodigoUsuario(int codigoUsuario)
        {
            var colaborador = await _colaboradorRepositorio.ObterColaboradorPorCodigoUsuario(codigoUsuario);

            if (colaborador == null)
                return null;

            var idiomasColaborador = await _idiomaServico.ListarIdiomaColaborador(colaborador.codigo_colaborador);
            var areasTrabalhoColaborador = await _areaTrabalhoServico.ListarAreasTrabalhoColaborador(colaborador.codigo_colaborador);
            var endereco = await _enderecoServico.ObterEndereco(colaborador.codigo_endereco);

            var resultado = new ColaboradorModel
            {
                CodigoColaborador = colaborador.codigo_colaborador,
                CodigoUsuario = colaborador.codigo_usuario,
                NomeColaborador = colaborador.nome_colaborador,
                DataAlteracao = colaborador.data_alteracao,
                DataCriacao = colaborador.data_criacao,
                EmailUsuario = colaborador.email_usuario,
                Nacionalidade = colaborador.nacionalidade,
                DataChegadaBrasil = colaborador.data_chegada_brasil,
                DataNascimento = colaborador.data_nascimento,
                Escolaridade = colaborador.escolaridade,
                AreaFormacao = colaborador.area_formacao,
                Entrevistado = colaborador.entrevistado,
                TelefoneUsuario = colaborador.telefone_usuario,
                Idiomas = idiomasColaborador.ToList(),
                AreasTrabalho = areasTrabalhoColaborador.ToList(),
                Endereco = endereco
            };

            return resultado;
        }
    }

    public interface IColaboradorSerivico
    {
        Task<int> CadastrarColaborador(ColaboradorModel colaborador);
        Task<ColaboradorModel> ObterColaboradorPorCodigoUsuario(int codigoUsuario);
        Task<List<ColaboradorModel>> ListarColaboradores();
        Task AtualizarColaborador(string nome, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade, int codigoUsuario, List<int> codigosIdiomas, List<int> codigosAreasTrabalho, EnderecoModel endereco);
    }
}
