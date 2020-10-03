﻿using Refugiados.BFF.Models;
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

        public ColaboradorServico(IColaboradorRepositorio colaboradorRepositorio)
        {
            _colaboradorRepositorio = colaboradorRepositorio;
        }

        public async Task AtualizarColaborador(string nome, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade, int codigoUsuario)
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
        }

        public async Task<int> CadastrarColaborador(ColaboradorModel colaborador)
        {
            await _colaboradorRepositorio.CadastrarColaborador(colaborador.NomeColaborador, colaborador.CodigoUsuario, colaborador.Nacionalidade, colaborador.DataNascimento,
                colaborador.DataChegadaBrasil, colaborador.AreaFormacao, colaborador.Escolaridade);

            var colaboradorCadastrado = await ObterColaboradorPorCodigoUsuario(colaborador.CodigoUsuario);

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
                EmailContato = colab.email_usuario
            }).ToList();

            return colaboradores;
        }

        public async Task<ColaboradorModel> ObterColaboradorPorCodigoUsuario(int codigoUsuario)
        {
            var colaborador = await _colaboradorRepositorio.ObterColaboradorPorCodigoUsuario(codigoUsuario);

            if (colaborador == null)
                return null;

            return new ColaboradorModel
            {
                CodigoColaborador = colaborador.codigo_colaborador,
                CodigoUsuario = colaborador.codigo_usuario,
                NomeColaborador = colaborador.nome_colaborador,
                DataAlteracao = colaborador.data_alteracao,
                DataCriacao = colaborador.data_criacao,
                EmailContato = colaborador.email_usuario
            };
        }
    }

    public interface IColaboradorSerivico
    {
        Task<int> CadastrarColaborador(ColaboradorModel colaborador);
        Task<ColaboradorModel> ObterColaboradorPorCodigoUsuario(int codigoUsuario);
        Task<List<ColaboradorModel>> ListarColaboradores();
        Task AtualizarColaborador(string nome, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade, int codigoUsuario);
    }
}
