using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Respostas;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarEmpresa()
        {
            var empresas = await _empresaServico.ListarEmpresas();

            return Ok(new RespostaModel
            {
                StatusCode = 200,
                Sucesso = true,
                Corpo = empresas
            });
        }

        [HttpPatch("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarEmpresa(int codigoUsuario, [FromBody] AtualizarEmpresaRequestModel request)
        {
            if (codigoUsuario <= 0)
            {
                return BadRequest(new RespostaModel
                {
                    StatusCode = 400,
                    Sucesso = false,
                    Mensagem = "Código inválido"
                });
            }

            var validacao = request.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new RespostaModel
                {
                    StatusCode = 400,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            await _empresaServico.AtualizarEmpresa(request.RazaoSocial, codigoUsuario);

            return Ok(new RespostaModel
            {
                StatusCode = 200,
                Sucesso = true,
                Corpo = new { codigoUsuario } 
            });
        }
    }
}
