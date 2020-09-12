using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Requisicoes;
using Refugiados.BFF.Servicos;
using System;
using System.Threading.Tasks;

namespace Refugiados.BFF.Controllers
{
    [ApiController]
    [Route("colaboradores")]
    public class ColaboradorControllercs : ControllerBase
    {
        private readonly IColaboradorSerivico _colaboradorServico;

        public ColaboradorControllercs(IColaboradorSerivico colaboradorServico)
        {
            _colaboradorServico = colaboradorServico;
        }

        [HttpGet("{codigoUsuario}")]
        public async Task<IActionResult> ObterColaboradorPorUsuario(int codigoUsuario)
        {
            var colaborador = await _colaboradorServico.ObterColaboradorPorCodigoUsuario(codigoUsuario);

            if (colaborador == null)
                return NotFound();

            return Ok(colaborador);
        }

        [HttpPatch("{codigoUsuario}")]
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
