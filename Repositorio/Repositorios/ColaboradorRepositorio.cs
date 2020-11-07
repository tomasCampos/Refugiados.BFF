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

        public async Task AtualizarColaborador(string nome, int codigoUsuario, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade)
        {            
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

        public async Task CadastrarColaborador(string nome, int codigoUsuario, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, 
            string escolaridade, int codigoEndereco)
        {
            await _dataBase.ExecutarAsync(AppConstants.CADASTRAR_COLABORADOR_SQL, new 
            { 
                nome_colaborador = nome, 
                codigo_usuario = codigoUsuario,
                nacionalidade,
                data_nascimento = dataNascimento,
                data_chegada_brasil = dataChegadaBrasil,
                area_formacao = areaFormacao,
                escolaridade,
                codigo_endereco = codigoEndereco
            });
        }

        public async Task<List<ColaboradorDto>> ListarColaboradores(string nacionalidade, string cidade, int? codigoIdioma, int? codigoAreaTrabalho)
        {
            var filtroNacionalidade = string.Empty;
            var filtroCidade = string.Empty;
            var filtroIdiomas = string.Empty;
            var filtroAreasTrabalho = string.Empty;

            var joinAreaTrabalho = string.Empty;
            var joinIdioma = string.Empty;

            if (!string.IsNullOrEmpty(nacionalidade))            
                filtroNacionalidade = $"AND c.nacionalidade LIKE '%{nacionalidade}%'";

            if (!string.IsNullOrEmpty(cidade))            
                filtroCidade = $"AND e.cidade_endereco LIKE '%{cidade}%'";

            if (codigoIdioma.HasValue)
            {
                filtroIdiomas = $"AND ci.codigo_idioma = '{codigoIdioma.Value}'";
                joinIdioma = "INNER JOIN colaborador_idioma AS ci ON ci.codigo_colaborador = c.codigo_colaborador";
            }

            if (codigoAreaTrabalho.HasValue)
            {
                filtroAreasTrabalho = $"AND cat.codigo_area_trabalho = '{codigoAreaTrabalho.Value}'";
                joinAreaTrabalho = "INNER JOIN colaborador_area_trabalho AS cat ON cat.codigo_colaborador = c.codigo_colaborador";
            }

            var query = string.Format(AppConstants.LISTAR_COLABORADORES_SQL, joinAreaTrabalho, joinIdioma, filtroNacionalidade, filtroCidade, filtroIdiomas, filtroAreasTrabalho);
            var colaboradores = await _dataBase.SelecionarAsync<ColaboradorDto>(query);
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
        Task CadastrarColaborador(string nome, int codigoUsuario, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaformacao, string escolaridade, int codigoEndereco);
        Task<ColaboradorDto> ObterColaboradorPorCodigoUsuario(int codigoUsuario);
        Task<List<ColaboradorDto>> ListarColaboradores(string nacionalidade, string cidade, int? codigoIdioma, int? codigoAreaTrabalho);
        Task AtualizarColaborador(string nome, int codigoUsuario, string nacionalidade, DateTime? dataNascimento, DateTime? dataChegadaBrasil, string areaFormacao, string escolaridade);
    }
}
