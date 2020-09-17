using Refugiados.BFF.Servicos;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Requisicoes;
using Refugiados.BFF.Servicos.Model;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ListarUsuarios(int? codigoUsuario, string emailUsuario = null)
        {
            if (codigoUsuario.HasValue && codigoUsuario.Value == 0)
            {
                return BadRequest("Informe um código válido");
            }

            var usuarios = await _usuarioServico.ListarUsuarios(codigoUsuario, emailUsuario);
            
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuarioAdmin([FromBody] UsuarioRequestModel requisicao)
        {
            if (requisicao == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(requisicao.EmailUsuario))
            {
                return BadRequest("O Email deve ser informado");
            }

            if (string.IsNullOrWhiteSpace(requisicao.SenhaUsuario))
            {
                return BadRequest("A senha deve ser informada");
            }

            var resultadoCadastro = await _usuarioServico.CadastrarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario);

            if (resultadoCadastro.SituacaoCadastro == CadastrarUsuarioServiceModel.SituacaoCadastroUsuario.NomeDeUsuarioJaUtilizado)
                return Ok(new { SucessoCadastro = false, Motivo = "Nome de usuário já utilizado" });
            else
                return Ok(new { SucessoCadastro = true, resultadoCadastro.CodigoUsuarioCadastrado });
        }

        [HttpPatch]
        public async Task<IActionResult> AtualizarUsuario([FromBody] AtualizarUsuarioRequestModel requisicao) 
        {
            if (requisicao == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(requisicao.EmailUsuario) && string.IsNullOrWhiteSpace(requisicao.SenhaUsuario))
            {
                return BadRequest("Nenhum dado para atualizar");
            }

            if (requisicao.CodigoUsuario <= 0)
            {
                return BadRequest("Usuario inexistente");
            }

            await _usuarioServico.AtualizarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario, requisicao.CodigoUsuario);

            return Ok();
        }

        [HttpPost("autenticacao")]
        public async Task<IActionResult> AutenticarUsuario([FromBody] UsuarioRequestModel requisicao)
        {
            if (requisicao == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(requisicao.EmailUsuario))
            {
                return BadRequest("O Email deve ser informado");
            }

            if (string.IsNullOrWhiteSpace(requisicao.SenhaUsuario))
            {
                return BadRequest("A senha deve ser informada");
            }

            var resultadoAutenticacao = await _usuarioServico.AutenticarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario);

            if (resultadoAutenticacao.SituacaoAutenticacao == AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.NomeDeUsuarioInvalido)
            {
                return Ok(new { SucessoAutenticacao = false, Motivo = "Usuario inexistente" });
            }

            if (resultadoAutenticacao.SituacaoAutenticacao == AutenticarUsuarioServiceModel.SituacaoAutenticacaoUsuario.SenhaInvalida)
            {
                return Ok(new { SucessoAutenticacao = false, Motivo = "Senha invalida" });
            }
            
            return Ok(new { SucessoAutenticacao = true, resultadoAutenticacao.CodigoUsuario });
        }
    }
}
