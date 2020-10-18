using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Respostas;
using Refugiados.BFF.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Refugiados.BFF.Controllers
{
    [ApiController]
    [Route("areas-trabalho")]
    public class AreaTrabalhoController : ControllerBase
    {
        private readonly IAreaTrabalhoServico _areaTrabalhoServico;

        public AreaTrabalhoController(IAreaTrabalhoServico areaTrabalhoServico)
        {
            _areaTrabalhoServico = areaTrabalhoServico;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarAreasTrabalho()
        {
            var areasTrabalho = await _areaTrabalhoServico.ListarAreasTrabalho();

            return Ok(new RespostaModel 
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Sucesso = true,
                Corpo = areasTrabalho
            });
        }
    }
}
