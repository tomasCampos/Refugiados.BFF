using Refugiados.BFF.Models;
using Repositorio.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Servicos
{
    public class AreaTrabalhoServico : IAreaTrabalhoServico
    {
        private readonly IAreaTrabalhoRepositorio _areaTrabalhoRepositorio;

        public AreaTrabalhoServico(IAreaTrabalhoRepositorio areaTrabalhoRepositorio)
        {
            _areaTrabalhoRepositorio = areaTrabalhoRepositorio;
        }

        public async Task<IEnumerable<AreaTrabalhoModel>> ListarAreasTrabalho()
        {
            var areasTrabalho = await _areaTrabalhoRepositorio.ListarAreasTrabalho();

            var resultado = areasTrabalho.Select(at => new AreaTrabalhoModel 
            {
                CodigoAreaTrabalho = at.codigo_area_trabalho,
                NomeAreaTrabalho = at.nome_area_trabalho
            }).ToList();

            return resultado;
        }
    }

    public interface IAreaTrabalhoServico
    {
        public Task<IEnumerable<AreaTrabalhoModel>> ListarAreasTrabalho();
    }
}
