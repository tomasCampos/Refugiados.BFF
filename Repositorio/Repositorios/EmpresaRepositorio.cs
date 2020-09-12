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
        public async Task AtualizarEmpresa(string razaoSocial, string codigoUsuario)
        {
            if (string.IsNullOrWhiteSpace(razaoSocial))
            {
                await _db.ExecutarAsync(AppConstants.ATUALIZAR_RAZAO_SOCIAL_EMPRESA, new { razaoSocial, codigoUsuario });
            }
        }

        public async Task CadastrarEmpresa(string razaoSocial, string codigoUsuario)
        {
            await _db.ExecutarAsync(AppConstants.CADASTRAR_EMPRESA, new { codigoUsuario, codigoEmpresa = Guid.NewGuid().ToString(), razaoSocial});
        }

        public async Task<EmpresaDto> ObterEmpresaPorId(string codigoEmpresa)
        {
            var response = await _db.SelecionarAsync<EmpresaDto>(AppConstants.OBTER_EMPRESA_POR_ID, new { codigoEmpresa });

            return response.FirstOrDefault();
        }
    }
}
