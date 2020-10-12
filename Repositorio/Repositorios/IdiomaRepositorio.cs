using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using System.Collections.Generic;
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
    }

    public interface IIdiomaRepositorio
    {
        Task<IEnumerable<IdiomaDto>> ListarIdioma();
    }
}
