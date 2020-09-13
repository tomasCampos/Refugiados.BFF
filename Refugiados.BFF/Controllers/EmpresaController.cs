using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models;
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

        [HttpPost]
        public async Task<IActionResult> CadastrarEmpresa([FromBody] EmpresaModel request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(request.RazaoSocial))
            {
                return BadRequest("O nome da empresa deve ser informado");
            }

            await _empresaServico.CadastrarEmpresa(request.RazaoSocial, request.CodigoUsuario);

            return Ok();
        }

        [HttpGet("{codigoUsuario}")]
        public async Task<IActionResult> ObterEmpresaPorUsuario(int codigoUsuario)
        {
            var empresa = await _empresaServico.ObterEmpresaPorCodigoUsuario(codigoUsuario);

            if (empresa == null)
                return NotFound();

            return Ok(empresa);
        }

        [HttpPatch("{codigoUsuario}")]
        public async Task<IActionResult> AtualizarEmpresa(int codigoUsuario, [FromBody] EmpresaModel request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(request.RazaoSocial))
            {
                return BadRequest("O nome da empresa deve ser informado");
            }

            await _empresaServico.AtualizarEmpresa(request.RazaoSocial, codigoUsuario);

            return Ok();
        }
    }
}
