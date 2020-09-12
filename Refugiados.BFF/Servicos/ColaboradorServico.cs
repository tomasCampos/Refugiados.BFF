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

        public ColaboradorServico(IColaboradorRepositorio colaboradorRepositorio)
        {
            _colaboradorRepositorio = colaboradorRepositorio;
        }

        public async Task AtualizarColaborador(string nome, int codigoUsuario)
        {
            await _colaboradorRepositorio.AtualizarColaborador(nome,codigoUsuario);
        }

        public async Task<int> CadastrarColaborador(string nome, int codigoUsuario)
        {
            await _colaboradorRepositorio.CadastrarColaborador(nome, codigoUsuario);
            var colaboradorCadastrado = await ObterColaboradorPorCodigoUsuario(codigoUsuario);

            return colaboradorCadastrado.CodigoColaborador;
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
                DataCriacao = colaborador.data_criacao
            };
        }
    }

    public interface IColaboradorSerivico
    {
        Task<int> CadastrarColaborador(string nome, int codigoUsuario);
        Task<ColaboradorModel> ObterColaboradorPorCodigoUsuario(int codigoUsuario);
        Task AtualizarColaborador(string nome, int codigoUsuario);
    }
}
