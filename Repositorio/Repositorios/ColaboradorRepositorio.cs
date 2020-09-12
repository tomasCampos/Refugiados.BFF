using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositorio.Repositorios
{
    public class ColaboradorRepositorio : IColaboradorRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        #region QUERIES 

        private const string OBTER_COLABORADOR_POR_CODIGO_USUARIO_SQL = @"SELECT * FROM heroku_93ac2d8811d872a.colaborador
                                                                   WHERE codigo_usuario = @codigo_usuario;";

        private const string CADASTRAR_COLABORADOR_SQL = @"INSERT INTO `heroku_93ac2d8811d872a`.`colaborador`
                                                    (`codigo_colaborador`,
                                                    `nome_colaborador`,
                                                    `data_criacao`,
                                                    `data_alteracao`,
                                                    `codigo_usuario`)
                                                    VALUES
                                                    (default,
                                                    @nome_colaborador,
                                                    default,
                                                    CURRENT_TIMESTAMP,
                                                    @codigo_usuario);";

        #endregion

        public ColaboradorRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public void AtualizarColaborador(string nome)
        {
            throw new NotImplementedException();
        }

        public void CadastrarColaborador(string nome, int codigoUsuario)
        {
            _dataBase.Executar(CADASTRAR_COLABORADOR_SQL, new { nome_colaborador = nome, codigo_usuario = codigoUsuario });
        }

        public ColaboradorDto ObterColaboradorPorCodigoUsuario(int codigoUsuario)
        {
            var colaborador = _dataBase.Selecionar<ColaboradorDto>(OBTER_COLABORADOR_POR_CODIGO_USUARIO_SQL, new { codigo_usuario = codigoUsuario }).FirstOrDefault();
            return colaborador;
        }
    }

    public interface IColaboradorRepositorio
    {
        void CadastrarColaborador(string nome, int codigoUsuario);
        ColaboradorDto ObterColaboradorPorCodigoUsuario(int codigoUsuario);
        void AtualizarColaborador(string nome);
    }
}
