using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Repositorios
{
    public class IdiomaRepositorio : IIdiomaRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        public IdiomaRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public async Task<IEnumerable<IdiomaDto>> ListarIdioma()
        {
            var idiomas = await _dataBase.SelecionarAsync<IdiomaDto>(AppConstants.LISTAR_IDIOMA);
            return idiomas;
        }

        public async Task<IEnumerable<IdiomaDto>> ListarIdiomaColaborador(int codigoColaborador)
        {
            var idiomas = await _dataBase.SelecionarAsync<IdiomaDto>(AppConstants.LISTAR_IDIOMA_COLABORADOR, new { codigo_colaborador = codigoColaborador });
            return idiomas;
        }

        public async Task<int> CadastrarIdiomaColaborador(int codigoColaborador, int codigoIdioma)
        {
            var linhasAfetadas = await _dataBase.ExecutarAsync(AppConstants.CADASTRAR_IDIOMA_COLABORADOR, new { codigo_colaborador = codigoColaborador, codigo_idioma = codigoIdioma });
            return linhasAfetadas;
        }

        public async Task<int> AtualizarIdiomaColaborador(int codigoColaborador, List<int> codigosIdiomas)
        {
            var linhasAfetadas = await _dataBase.ExecutarAsync(AppConstants.EXCLUIR_IDIOMA_COLABORADOR, new { codigo_colaborador = codigoColaborador });

            foreach (var codigoIdioma in codigosIdiomas)
            {
                await CadastrarIdiomaColaborador(codigoColaborador, codigoIdioma);
            }

            return linhasAfetadas;
        }
    }

    public interface IIdiomaRepositorio
    {
        Task<IEnumerable<IdiomaDto>> ListarIdioma();
        Task<IEnumerable<IdiomaDto>> ListarIdiomaColaborador(int codigoColaborador);
        Task<int> CadastrarIdiomaColaborador(int codigoColaborador, int codigoIdioma);
        Task<int> AtualizarIdiomaColaborador(int codigoColaborador, List<int> codigosIdiomas);
    }
}
