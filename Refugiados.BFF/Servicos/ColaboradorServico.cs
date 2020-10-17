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

        public ColaboradorServico(IColaboradorRepositorio colaboradorRepositorio, IIdiomaServico idiomaServico)
        {
            _colaboradorRepositorio = colaboradorRepositorio;
            _idiomaServico = idiomaServico;
        }

        public async Task AtualizarColaborador(string nome, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade, 
            int codigoUsuario, List<int> codigosIdiomas)
        {
            var colaborador = await ObterColaboradorPorCodigoUsuario(codigoUsuario);

            if (colaborador == null)
                return;

            colaborador.NomeColaborador = string.IsNullOrEmpty(nome) ? colaborador.NomeColaborador : nome;
            colaborador.Nacionalidade = string.IsNullOrEmpty(nacionalidade) ? colaborador.Nacionalidade : nacionalidade;
            colaborador.DataNascimento = !dataNascimento.HasValue ? colaborador.DataNascimento : dataNascimento;
            colaborador.DataChegadaBrasil = !dataChegadaBrasil.HasValue ? colaborador.DataChegadaBrasil : dataChegadaBrasil;            
            colaborador.AreaFormacao = string.IsNullOrEmpty(areaFormacao) ? colaborador.AreaFormacao : areaFormacao;
            colaborador.Escolaridade = string.IsNullOrEmpty(escolaridade) ? colaborador.Escolaridade : escolaridade;

            await _colaboradorRepositorio.AtualizarColaborador(colaborador.NomeColaborador, colaborador.CodigoUsuario, colaborador.Nacionalidade, colaborador.DataNascimento, 
                colaborador.DataChegadaBrasil, colaborador.AreaFormacao, colaborador.Escolaridade);

            if (codigosIdiomas != null && codigosIdiomas.Any())
            {
                var listaIdiomas = new List<IdiomaModel>();
                foreach (var codigo in codigosIdiomas)
                {
                    listaIdiomas.Add(new IdiomaModel { CodigoIdioma = codigo });
                }

                await _idiomaServico.CadastrarAtualizarIdiomaColaborador(colaborador.CodigoColaborador, listaIdiomas);
            }
        }

        public async Task<int> CadastrarColaborador(ColaboradorModel colaborador)
        {
            await _colaboradorRepositorio.CadastrarColaborador(colaborador.NomeColaborador, colaborador.CodigoUsuario, colaborador.Nacionalidade, colaborador.DataNascimento,
                colaborador.DataChegadaBrasil, colaborador.AreaFormacao, colaborador.Escolaridade);

            var colaboradorCadastrado = await ObterColaboradorPorCodigoUsuario(colaborador.CodigoUsuario);

            await _idiomaServico.CadastrarAtualizarIdiomaColaborador(colaboradorCadastrado.CodigoColaborador, colaborador.Idiomas);

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
                Entrevistado = colab.entrevistado
            }).ToList();

            foreach (var colaborador in colaboradores)
            {
                var idiomasColaborador = await _idiomaServico.ListarIdiomaColaborador(colaborador.CodigoColaborador);
                colaborador.Idiomas = idiomasColaborador.ToList();
            }

            return colaboradores;
        }

        public async Task<ColaboradorModel> ObterColaboradorPorCodigoUsuario(int codigoUsuario)
        {
            var colaborador = await _colaboradorRepositorio.ObterColaboradorPorCodigoUsuario(codigoUsuario);

            var idiomasColaborador = await _idiomaServico.ListarIdiomaColaborador(colaborador.codigo_colaborador);

            if (colaborador == null)
                return null;

            return new ColaboradorModel
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
                Idiomas = idiomasColaborador.ToList()
            };
        }
    }

    public interface IColaboradorSerivico
    {
        Task<int> CadastrarColaborador(ColaboradorModel colaborador);
        Task<ColaboradorModel> ObterColaboradorPorCodigoUsuario(int codigoUsuario);
        Task<List<ColaboradorModel>> ListarColaboradores();
        Task AtualizarColaborador(string nome, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade, int codigoUsuario, List<int> codigosIdiomas);
    }
}
