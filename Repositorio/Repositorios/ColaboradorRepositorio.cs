using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class ColaboradorRepositorio : IColaboradorRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        public ColaboradorRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public async Task AtualizarColaborador(string nome,int codigoUsuario, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade)
        {
            if (!string.IsNullOrWhiteSpace(nome))
                await _dataBase.ExecutarAsync(AppConstants.ATUALIZAR_COLABORADOR, new 
                { 
                    nome_colaborador = nome,
                    nacionalidade,
                    data_nascimento = dataNascimento,
                    data_chegada_brasil = dataChegadaBrasil,
                    area_formacao = areaFormacao,
                    escolaridade,
                    codigo_usuario = codigoUsuario
                });            
        }

        public async Task CadastrarColaborador(string nome, int codigoUsuario, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade)
        {
            await _dataBase.ExecutarAsync(AppConstants.CADASTRAR_COLABORADOR_SQL, new 
            { 
                nome_colaborador = nome, 
                codigo_usuario = codigoUsuario,
                nacionalidade,
                data_nascimento = dataNascimento,
                data_chegada_brasil = dataChegadaBrasil,
                area_formacao = areaFormacao,
                escolaridade
            });
        }

        public async Task<List<ColaboradorDto>> ListarColaboradores()
        {
            var colaboradores = await _dataBase.SelecionarAsync<ColaboradorDto>(AppConstants.LISTAR_COLABORADORES_SQL);
            return colaboradores.ToList();
        }

        public async Task<ColaboradorDto> ObterColaboradorPorCodigoUsuario(int codigoUsuario)
        {
            var colaborador = await _dataBase.SelecionarAsync<ColaboradorDto>(AppConstants.OBTER_COLABORADOR_POR_CODIGO_USUARIO_SQL, new { codigo_usuario = codigoUsuario });
            return colaborador.FirstOrDefault();
        }
    }

    public interface IColaboradorRepositorio
    {
        Task CadastrarColaborador(string nome, int codigoUsuario, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaformacao, string escolaridade);
        Task<ColaboradorDto> ObterColaboradorPorCodigoUsuario(int codigoUsuario);
        Task<List<ColaboradorDto>> ListarColaboradores();
        Task AtualizarColaborador(string nome, int codigoUsuario, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade);
    }
}
