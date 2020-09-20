using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Colaborador.Requisicoes;
using Refugiados.BFF.Models.Respostas;
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
                Corpo = colaborador 
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarColaborador()
        {
            var colaboradores = await _colaboradorServico.ListarColaboradores();

            return Ok(new RespostaModel
            {
                StatusCode = 200,
                Sucesso = true,
                Corpo = colaboradores
            });
        }

        [HttpPatch("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarColaborador(int codigoUsuario, [FromBody] AtualizarColaboradorRequestModel colaborador)
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

            var validacao = colaborador.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new RespostaModel
                {
                    StatusCode = 400,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            await _colaboradorServico.AtualizarColaborador(colaborador.NomeColaborador, codigoUsuario);

            return Ok(new RespostaModel 
            {
                StatusCode = 200,
                Sucesso = true,
                Corpo = new { codigoUsuario }
            });
        }
    }
}
