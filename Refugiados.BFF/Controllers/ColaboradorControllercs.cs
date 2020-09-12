using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Requisicoes;
using Refugiados.BFF.Servicos;
using System;

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
        public IActionResult ObterColaboradorPorUsuario(int codigoUsuario)
        {
            var colaborador = _colaboradorServico.ObterColaboradorPorCodigoUsuario(codigoUsuario);

            if (colaborador == null)
                return NotFound();

            return Ok(colaborador);
        }

        [HttpPatch("{codigoUsuario}")]
        public IActionResult AlterarColaborador(int codigoUsuario, [FromBody] AtualizarColaboradorRequestModel colaborador)
        {
            throw new NotImplementedException();
        }
    }
}
