using Refugiados.BFF.Servicos;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Usuario.Requisicoes;
using Refugiados.BFF.Servicos.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Refugiados.BFF.Models.Respostas;
using Refugiados.BFF.Models.Requisicoes.Usuario;
using System.Net;

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
                return BadRequest(new RespostaModel 
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = "Informe um código válido",
                });

            var usuarios = await _usuarioServico.ListarUsuarios(codigoUsuario, emailUsuario);

            return Ok(new RespostaModel 
            {
                StatusCode = HttpStatusCode.OK,
                Sucesso = true,
                Mensagem = string.Empty,
                Corpo = usuarios
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioRequestModel requisicao)
        {
            var validacao = requisicao.Validar();
            if(!validacao.Valido)
            {
                return BadRequest(new RespostaModel 
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            var resultadoCadastro = await _usuarioServico.CadastrarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario);
            return FormatarResultadoCadastroOuAtualizacaoUsuario(resultadoCadastro);
        }

        [HttpPost("colaborador")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuarioColaborador([FromBody] UsuarioColaboradorRequestModel requisicao)
        {
            var validacao = requisicao.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new RespostaModel
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            var resultadoCadastro = await _usuarioServico.CadastrarUsuarioColaborador(requisicao.EmailUsuario, requisicao.SenhaUsuario, requisicao.NomeColaborador);
            return FormatarResultadoCadastroOuAtualizacaoUsuario(resultadoCadastro);
        }

        [HttpPost("empresa")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuarioEmpresa([FromBody] UsuarioEmpresaRequestModel requisicao)
        {
            var validacao = requisicao.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new RespostaModel
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            var resultadoCadastro = await _usuarioServico.CadastrarUsuarioEmpresa(requisicao.EmailUsuario, requisicao.SenhaUsuario, requisicao.RazaoSocial, requisicao.CNPJ, requisicao.NomeFantasia, requisicao.DataFundacao, requisicao.NumeroFuncionarios);
            return FormatarResultadoCadastroOuAtualizacaoUsuario(resultadoCadastro);
        }

        [HttpPatch("{codigoUsuario}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarUsuario([FromBody] AtualizarUsuarioRequestModel requisicao, int codigoUsuario)
        {
            if (codigoUsuario <= 0)
            {
                return BadRequest(new RespostaModel
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = "Usuário inexistente"
                });
            }

            var validacao = requisicao.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new RespostaModel
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            var resultadoCadastro = await _usuarioServico.AtualizarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario, codigoUsuario);
            return FormatarResultadoCadastroOuAtualizacaoUsuario(resultadoCadastro, false);
        }

        [HttpPost("autenticacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AutenticarUsuario([FromBody] UsuarioRequestModel requisicao)
        {
            var validacao = requisicao.Validar();
            if (!validacao.Valido)
            {
                return BadRequest(new RespostaModel
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Sucesso = false,
                    Mensagem = validacao.MensagemDeErro
                });
            }

            var resultadoAutenticacao = await _usuarioServico.AutenticarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario);

            if (resultadoAutenticacao.SituacaoAutenticacao == AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.NomeDeUsuarioInvalido)
            {
                return Ok(new RespostaModel
                {
                    StatusCode = HttpStatusCode.OK,
                    Sucesso = false,
                    Mensagem = "Usuario inexistente"
                });
            }

            if (resultadoAutenticacao.SituacaoAutenticacao == AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.SenhaInvalida)
            {
                return Ok(new RespostaModel
                {
                    StatusCode = HttpStatusCode.OK,
                    Sucesso = false,
                    Mensagem = "Senha Invalida"
                });
            }

            return Ok(new RespostaModel 
            {
                StatusCode = HttpStatusCode.OK,
                Sucesso = true,
                Corpo = new { resultadoAutenticacao.CodigoUsuario, resultadoAutenticacao.PerfilUsuario }
            });
        }

        #region Metodos privados

        private IActionResult FormatarResultadoCadastroOuAtualizacaoUsuario(CadastrarAtualizarUsuarioServiceModel resultadoCadastro, bool cadastro = true )
        {
            if (resultadoCadastro.SituacaoCadastro == CadastrarAtualizarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado)
            {
                return Conflict(new RespostaModel
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Sucesso = false,
                    Mensagem = "Nome de usuário já utilizado"
                });
            }
            else if (cadastro)
            {
                return Created($"/usuarios/{resultadoCadastro.CodigoUsuarioCadastrado}", new RespostaModel
                {
                    StatusCode = HttpStatusCode.Created,
                    Sucesso = true,
                    Corpo = new { CodigoUsuario = resultadoCadastro.CodigoUsuarioCadastrado } 
                });
            }

            return Ok(new RespostaModel
            {
                StatusCode = HttpStatusCode.OK,
                Sucesso = true,
                Corpo = new { CodigoUsuario = resultadoCadastro.CodigoUsuarioCadastrado }
            });
        }

        #endregion
    }
}
