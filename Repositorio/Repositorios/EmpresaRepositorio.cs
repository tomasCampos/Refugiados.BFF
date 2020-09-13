using System;
using System.Linq;
using System.Threading.Tasks;
using Repositorio.CrossCutting;
using Repositorio.Dtos;
using Repositorio.Insfraestrutura;
using Repositorio.Repositorios.Interfaces;

namespace Repositorio.Repositorios
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly DataBaseConnector _db;

        public EmpresaRepositorio()
        {
            _db = new DataBaseConnector();
        }
        public async Task AtualizarEmpresa(string razaoSocial, int codigoUsuario)
        {
            if (!string.IsNullOrWhiteSpace(razaoSocial))
            {
                await _db.ExecutarAsync(AppConstants.ATUALIZAR_RAZAO_SOCIAL_EMPRESA, new { razao_social = razaoSocial, codigo_usuario = codigoUsuario });
            }
        }

        public async Task CadastrarEmpresa(string razaoSocial, int codigoUsuario)
        {
            await _db.ExecutarAsync(AppConstants.CADASTRAR_EMPRESA, new { codigo_usuario = codigoUsuario, razao_social = razaoSocial});
        }

        public async Task<EmpresaDto> ObterEmpresaPorCodigoUsuario(int codigoUsuario)
        {
            var response = await _db.SelecionarAsync<EmpresaDto>(AppConstants.OBTER_EMPRESA_POR_CODIGO_USUARIO, new { codigo_usuario = codigoUsuario });

            return response.FirstOrDefault();
        }
    }
}
