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

            return idiomas.OrderBy(i => i.DescricaoIdioma).ToList();
        }

        public async Task<IEnumerable<IdiomaModel>> ListarIdiomaColaborador(int codigoColaborador)
        {
            var lista = await _idiomaRepositorio.ListarIdiomaColaborador(codigoColaborador);

            var idiomas = lista.Select(idioma => new IdiomaModel
            {
                CodigoIdioma = idioma.codigo_idioma,
                DescricaoIdioma = idioma.nome_idioma
            });

            return idiomas.OrderBy(i => i.DescricaoIdioma).ToList();
        }

        public async Task CadastrarAtualizarIdiomaColaborador(int codigoColaborador, List<IdiomaModel> Idiomas)
        {
            var idiomasValidos = await ListarIdioma();
            var idiomasParaCadastrar = Idiomas.Select(i => i.CodigoIdioma).Intersect(idiomasValidos.Select(iv => iv.CodigoIdioma));

            await _idiomaRepositorio.CadastrarAtualizarIdiomaColaborador(codigoColaborador, idiomasParaCadastrar.ToList());
        }
    }

    public interface IIdiomaServico 
    {
        Task<IEnumerable<IdiomaModel>> ListarIdioma();
        Task<IEnumerable<IdiomaModel>> ListarIdiomaColaborador(int codigoColaborador);
        Task CadastrarAtualizarIdiomaColaborador(int codigoColaborador, List<IdiomaModel> codigosIdioma);
    }
}
