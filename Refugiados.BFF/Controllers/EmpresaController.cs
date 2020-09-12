using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models;
using Refugiados.BFF.Servicos.Interfaces;

namespace Refugiados.BFF.Controllers
{
    [ApiController]
    [Route("empresa")]
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

            await _empresaServico.CadastrarEmpresa(request.RazaoSocial, request.CodigoUsuario.ToString());

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ObterEmpresaPorId(string codigoEmpresa)
        {
            if (string.IsNullOrWhiteSpace(codigoEmpresa))
            {
                return BadRequest("O código da empresa deve ser informado");
            }

            await _empresaServico.ObterEmpresaPorId(codigoEmpresa);

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> AtualizarEmpresa([FromBody] EmpresaModel request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(request.RazaoSocial))
            {
                return BadRequest("O nome da empresa deve ser informado");
            }

            await _empresaServico.AtualizarEmpresa(request.RazaoSocial, request.CodigoUsuario.ToString());

            return Ok();
        }
    }
}
