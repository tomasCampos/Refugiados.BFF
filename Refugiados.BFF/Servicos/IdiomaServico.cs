using Refugiados.BFF.Models;
using Repositorio.Repositorios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos
{
    public class IdiomaServico : IIdiomaServico
    {
        private readonly IIdiomaRepositorio _idiomaRepositorio;

        public IdiomaServico(IIdiomaRepositorio idiomaRepositorio)
        {
            _idiomaRepositorio = idiomaRepositorio;
        }

        public async Task<IEnumerable<IdiomaModel>> ListarIdioma()
        {
            var lista = await _idiomaRepositorio.ListarIdioma();

            var idiomas = lista.Select(idioma => new IdiomaModel 
            {
                CodigoIdioma = idioma.codigo_idioma,
                DescricaoIdioma = idioma.nome_idioma
            });

            return idiomas.ToList().OrderBy(i => i.DescricaoIdioma);
        }
    }

    public interface IIdiomaServico 
    {
        Task<IEnumerable<IdiomaModel>> ListarIdioma();
    }
}
