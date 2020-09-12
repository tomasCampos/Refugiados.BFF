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

        public void AtualizarColaborador(string nome, int codigoUsuario)
        {
            _colaboradorRepositorio.AtualizarColaborador(nome,codigoUsuario);
        }

        public int CadastrarColaborador(string nome, int codigoUsuario)
        {
            _colaboradorRepositorio.CadastrarColaborador(nome, codigoUsuario);
            var colaboradorCadastrado = ObterColaboradorPorCodigoUsuario(codigoUsuario);

            return colaboradorCadastrado.CodigoColaborador;
        }


        public ColaboradorModel ObterColaboradorPorCodigoUsuario(int codigoUsuario)
        {
            var colaborador = _colaboradorRepositorio.ObterColaboradorPorCodigoUsuario(codigoUsuario);

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
        int CadastrarColaborador(string nome, int codigoUsuario);
        ColaboradorModel ObterColaboradorPorCodigoUsuario(int codigoUsuario);
        void AtualizarColaborador(string nome, int codigoUsuario);
    }
}
