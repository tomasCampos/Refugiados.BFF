using Refugiados.BFF.Servicos;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Usuario.Requisicoes;
using Refugiados.BFF.Servicos.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Refugiados.BFF.Models.Requisicoes.Usuario;

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
            return FormatarResultadoCadastroOuAtualizacaoUsuario(resultadoCadastro);
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
            return FormatarResultadoCadastroOuAtualizacaoUsuario(resultadoCadastro);
        }

        [HttpPost("empresa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuarioEmpresa([FromBody] UsuarioEmpresaRequestModel requisicao)
        {
            if (requisicao == null || !ModelState.IsValid)
                return BadRequest();

            var resultadoCadastro = await _usuarioServico.CadastrarUsuarioEmpresa(requisicao.EmailUsuario, requisicao.SenhaUsuario, requisicao.RazaoSocial);
            return FormatarResultadoCadastroOuAtualizacaoUsuario(resultadoCadastro);
        }

        [HttpPatch("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
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

            var resultadoCadastro = await _usuarioServico.AtualizarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario, codigoUsuario);

            return FormatarResultadoCadastroOuAtualizacaoUsuario(resultadoCadastro, false);
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

        private IActionResult FormatarResultadoCadastroOuAtualizacaoUsuario(CadastrarAtualizarUsuarioServiceModel resultadoCadastro, bool cadastro = true )
        {
            if (resultadoCadastro.SituacaoCadastro == CadastrarAtualizarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado)
                return Conflict(new { Mensagem = "Nome de usuário já utilizado", CodigoUsuarioCadastrado = 0 });
            else if(cadastro)
                return Created($"/usuarios/{resultadoCadastro.CodigoUsuarioCadastrado}", new { Mensagem = "Cadastrado com sucesso", resultadoCadastro.CodigoUsuarioCadastrado });

            return Ok(new { Mensagem = "Cadastrado com sucesso", resultadoCadastro.CodigoUsuarioCadastrado });
        }

        #endregion
    }
}
