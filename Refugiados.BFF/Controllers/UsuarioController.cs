using Refugiados.BFF.Servicos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Refugiados.BFF.Models;
using Refugiados.BFF.Models.Requisicoes;

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
        public IActionResult ListarUsuarios(int? codigoUsuario, string emailUsuario = null)
        {
            if (codigoUsuario.HasValue && codigoUsuario.Value == 0)
                return BadRequest("Informe um código válido");

            var usuarios = _usuarioServico.ListarUsuarios(codigoUsuario, emailUsuario);
            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] CadastrarUsuarioRequestModel requisicao)
        {
            if (requisicao == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(requisicao.EmailUsuario))
                return BadRequest("O Email deve ser informado");

            if (string.IsNullOrWhiteSpace(requisicao.SenhaUsuario))
                return BadRequest("A senha deve ser informada");

            _usuarioServico.CadastrarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario);

            return Ok();
        }

        [HttpPatch]
        public IActionResult AtualizarUsuario([FromBody] AtualizarUsuarioRequisicaoModelcs requisicao) 
        {
            if (requisicao == null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(requisicao.EmailUsuario) && string.IsNullOrWhiteSpace(requisicao.SenhaUsuario))
                return BadRequest("Nenhum dado para atualizar");

            if (requisicao.CodigoUsuario <= 0)
                return BadRequest("Usuario inexistente");

            _usuarioServico.AtualizarUsuario(requisicao.EmailUsuario, requisicao.SenhaUsuario, requisicao.CodigoUsuario);

            return Ok();
        }
    }
}
