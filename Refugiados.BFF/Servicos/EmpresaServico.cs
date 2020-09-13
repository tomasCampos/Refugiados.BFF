using System.Threading.Tasks;
using Refugiados.BFF.Models;
using Refugiados.BFF.Servicos.Interfaces;
using Repositorio.Repositorios.Interfaces;

namespace Refugiados.BFF.Servicos
{
    public class EmpresaServico : IEmpresaServico
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;

        public EmpresaServico(IEmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        public async Task CadastrarEmpresa(string razaoSocial, int codigoUsuario)
        {
            await _empresaRepositorio.CadastrarEmpresa(razaoSocial, codigoUsuario);
        }

        public async Task AtualizarEmpresa(string razaoSocial, int codigoUsuario)
        {
            await _empresaRepositorio.AtualizarEmpresa(razaoSocial, codigoUsuario);
        }

        public async Task<EmpresaModel> ObterEmpresaPorCodigoUsuario(int codigoUsuario)
        {
            var empresa = await _empresaRepositorio.ObterEmpresaPorCodigoUsuario(codigoUsuario);

            if (empresa != null)
            {
                return new EmpresaModel
                {
                    CodigoEmpresa = empresa.codigo_empresa,
                    CodigoUsuario = empresa.codigo_usuario,
                    RazaoSocial = empresa.razao_social
                };
            }
            else
            {
                return null;
            }
        }
    }
}

