using Refugiados.BFF.Servicos;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Requisicoes;
using Refugiados.BFF.Servicos.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Refugiados.BFF.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ListarUsuarios(int? codigoUsuario, string emailUsuario = null)
        {
            if (codigoUsuario.HasValue && codigoUsuario.Value == 0)
                return BadRequest("Informe um código válido");

            var usuarios = await _usuarioServico.ListarUsuarios(codigoUsuario, emailUsuario);

            return Ok(usuarios);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioRequestModel requisicao)
        {
            if (requisicao == null || !ModelState.IsValid)
                return BadRequest();

            var resultadoCadastro = await _usuarioServico.CadastrarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario);
            return FormatarResultadoCadastroUsuario(resultadoCadastro);
        }

        [HttpPost("colaborador")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuarioColaborador([FromBody] UsuarioColaboradorRequestModel requisicao)
        {
            if (requisicao == null || !ModelState.IsValid)
                return BadRequest();

            var resultadoCadastro = await _usuarioServico.CadastrarUsuarioColaborador(requisicao.EmailUsuario, requisicao.SenhaUsuario, requisicao.NomeColaborador);
            return FormatarResultadoCadastroUsuario(resultadoCadastro);
        }


        [HttpPatch("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarUsuario([FromBody] AtualizarUsuarioRequestModel requisicao, int codigoUsuario)
        {
            if (requisicao == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(requisicao.EmailUsuario) && string.IsNullOrWhiteSpace(requisicao.SenhaUsuario))
            {
                return BadRequest("Nenhum dado para atualizar");
            }

            if (codigoUsuario <= 0)
            {
                return BadRequest("Usuario inexistente");
            }

            await _usuarioServico.AtualizarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario, codigoUsuario);

            return Ok();
        }

        [HttpPost("autenticacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AutenticarUsuario([FromBody] UsuarioRequestModel requisicao)
        {
            if (requisicao == null || !ModelState.IsValid)
                return BadRequest();

            var resultadoAutenticacao = await _usuarioServico.AutenticarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario);

            if (resultadoAutenticacao.SituacaoAutenticacao == AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.NomeDeUsuarioInvalido)
            {
                return Ok(new { SucessoAutenticacao = false, Motivo = "Usuario inexistente" });
            }

            if (resultadoAutenticacao.SituacaoAutenticacao == AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.SenhaInvalida)
            {
                return Ok(new { SucessoAutenticacao = false, Motivo = "Senha invalida" });
            }

            return Ok(new { SucessoAutenticacao = true, resultadoAutenticacao.CodigoUsuario, resultadoAutenticacao.PerfilUsuario });
        }

        #region Metodos privados

        private IActionResult FormatarResultadoCadastroUsuario(CadastrarUsuarioServiceModel resultadoCadastro)
        {
            if (resultadoCadastro.SituacaoCadastro == CadastrarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado)
                return Conflict("Nome de usuário já utilizado");
            else
                return Created($"/usuarios/{resultadoCadastro.CodigoUsuarioCadastrado}", resultadoCadastro.CodigoUsuarioCadastrado);
        }

        #endregion
    }
}
