using Autofac;
using Refugiados.BFF.Servicos;
using Refugiados.BFF.Servicos.Interfaces;
using Repositorio.Repositorios;
using Repositorio.Repositorios.Interfaces;

namespace Refugiados.BFF
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmpresaServico>().As<IEmpresaServico>();
            builder.RegisterType<EmpresaRepositorio>().As<IEmpresaRepositorio>();
        }
    }
}
