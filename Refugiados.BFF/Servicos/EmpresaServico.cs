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

        public async Task CadastrarEmpresa(string razaoSocial, string codigoUsuario)
        {
            await _empresaRepositorio.CadastrarEmpresa(razaoSocial, codigoUsuario);
        }

        public async Task AtualizarEmpresa(string razaoSocial, string codigoUsuario)
        {
            await _empresaRepositorio.AtualizarEmpresa(razaoSocial, codigoUsuario);
        }

        public async Task <EmpresaModel> ObterEmpresaPorId(string id)
        {
            var empresa = await _empresaRepositorio.ObterEmpresaPorId(id);

            if (empresa != null)
            {
                return new EmpresaModel
                {
                    CodigoEmpresa = empresa.CodigoEmpresa,
                    CodigoUsuario = int.Parse(empresa.CodigoUsuario),
                    RazaoSocial = empresa.RazaoSocial
                };
            }
            else
            {
                return new EmpresaModel();
            }
        }
    }
}

