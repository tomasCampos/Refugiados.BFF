
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models;
using Refugiados.BFF.Servicos;

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
        public UsuarioModel ObterUsuario()
        {
            var usuario = _usuarioServico.ObterUsuario();
            return usuario;
        }
    }
}
