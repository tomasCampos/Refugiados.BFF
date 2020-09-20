using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models;
using Refugiados.BFF.Models.Respostas;
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarEmpresa([FromBody] EmpresaModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _empresaServico.CadastrarEmpresa(request.RazaoSocial, request.CodigoUsuario);

            return StatusCode(201);
        }

        [HttpGet("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterEmpresaPorUsuario(int codigoUsuario)
        {
            var empresa = await _empresaServico.ObterEmpresaPorCodigoUsuario(codigoUsuario);

            if (empresa == null)
            {
                return NotFound(new RespostaModel 
                {
                    StatusCode = 404,
                    Sucesso = false,
                    Mensagem = "Não encontrado"
                });
            }

            return Ok(new RespostaModel 
            {
                StatusCode = 200,
                Sucesso = true,
                Corpo = empresa
            });
        }

        [HttpPatch("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarEmpresa(int codigoUsuario, [FromBody] EmpresaModel request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _empresaServico.AtualizarEmpresa(request.RazaoSocial, codigoUsuario);

            return Ok(new RespostaModel
            {
                StatusCode = 200,
                Sucesso = true,
                Corpo = codigoUsuario
            });
        }
    }
}
