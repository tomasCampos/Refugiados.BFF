using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Colaborador.Requisicoes;
using Refugiados.BFF.Servicos;
using System;
using System.Threading.Tasks;

namespace Refugiados.BFF.Controllers
{
    [ApiController]
    [Route("colaboradores")]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorSerivico _colaboradorServico;

        public ColaboradorController(IColaboradorSerivico colaboradorServico)
        {
            _colaboradorServico = colaboradorServico;
        }

        [HttpGet("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterColaboradorPorUsuario(int codigoUsuario)
        {
            var colaborador = await _colaboradorServico.ObterColaboradorPorCodigoUsuario(codigoUsuario);

            if (colaborador == null)
                return NotFound();

            return Ok(colaborador);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarColaborador()
        {
            var colaborador = await _colaboradorServico.ListarColaboradores();

            return Ok(colaborador);
        }

        [HttpPatch("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarColaborador(int codigoUsuario, [FromBody] AtualizarColaboradorRequestModel colaborador)
        {
            if (colaborador == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(colaborador.NomeColaborador))
                return BadRequest("Nenhum dado para atualizar");

            if (codigoUsuario <= 0)
                return BadRequest("Usuario inexistente");

            await _colaboradorServico.AtualizarColaborador(colaborador.NomeColaborador, codigoUsuario);

            return Ok();
        }
    }
}
