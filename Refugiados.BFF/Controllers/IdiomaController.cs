using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refugiados.BFF.Models.Respostas;
using Refugiados.BFF.Servicos;
using System.Threading.Tasks;

namespace Refugiados.BFF.Controllers
{
    [ApiController]
    [Route("idiomas")]
    public class IdiomaController : ControllerBase
    {
        private readonly IIdiomaServico _idiomaServico;

        public IdiomaController(IIdiomaServico idiomaServico)
        {
            _idiomaServico = idiomaServico;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarIdiomas()
        {
            var idiomas = await _idiomaServico.ListarIdioma();

            return Ok(new RespostaModel 
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Sucesso = true,
                Corpo = idiomas
            });
        }
    }
}
