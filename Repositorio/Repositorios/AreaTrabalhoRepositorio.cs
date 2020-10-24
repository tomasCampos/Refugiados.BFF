using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Repositorio.Repositorios
{
    public class AreaTrabalhoRepositorio : IAreaTrabalhoRepositorio
    {
        private readonly DataBaseConnector _dataBase;

        public AreaTrabalhoRepositorio()
        {
            _dataBase = new DataBaseConnector();
        }

        public async Task<IEnumerable<AreaTrabalhoDto>> ListarAreasTrabalho()
        {
            var areasTrabalho = await _dataBase.SelecionarAsync<AreaTrabalhoDto>(AppConstants.LISTAR_AREA_TRABALHO);
            var retorno = areasTrabalho.OrderBy(at => at.nome_area_trabalho).ToList();

            return retorno;
        }

        public async Task<IEnumerable<AreaTrabalhoDto>> ListarAreasTrabalhoColaborador(int codigoColaborador)
        {
            var areasTrabalho = await _dataBase.SelecionarAsync<AreaTrabalhoDto>(AppConstants.LISTAR_AREA_TRABALHO_COLABORADOR, new { codigo_colaborador = codigoColaborador });
            return areasTrabalho;
        }
    }

    public interface IAreaTrabalhoRepositorio
    {
        Task<IEnumerable<AreaTrabalhoDto>> ListarAreasTrabalho();
        Task<IEnumerable<AreaTrabalhoDto>> ListarAreasTrabalhoColaborador(int codigoColaborador);
    }
}
