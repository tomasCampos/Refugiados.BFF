using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models;
using Refugiados.BFF.Models.Requisicoes.Empresa;
using Refugiados.BFF.Servicos.Interfaces;

namespace Refugiados.BFF.Controllers
{
    [ApiController]
    [Route("empresas")]
    public class EmpresaController : Controller
    {
        public readonly IEmpresaServico _empresaServico;
        public EmpresaController(IEmpresaServico empresaServico)
        {
            _empresaServico = empresaServico;
        }

        [HttpGet("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterEmpresaPorUsuario(int codigoUsuario)
        {
            var empresa = await _empresaServico.ObterEmpresaPorCodigoUsuario(codigoUsuario);

            if (empresa == null)
            {
                return NotFound();
            }

            return Ok(empresa);
        }

        [HttpPatch("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarEmpresa(int codigoUsuario, [FromBody] AtualizarEmpresaRequestModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _empresaServico.AtualizarEmpresa(request.RazaoSocial, codigoUsuario);

            return Ok();
        }
    }
}
