using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Interfaces.Service;
using GoBolao.Domain.Usuarios.Services;
using GoBolao.Infra.Data.Contextos;
using GoBolao.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace GoBolao.Infra.CrossCuting.IOC
{
    public class NativeInjector
    {
        public static void ResolverDependencias(IServiceCollection service)
        {

            service.AddScoped<ContextoMSSQL>();
            service.AddScoped<IRepositoryUsuario, RepositoryUsuario>();
            service.AddScoped<IServiceUsuario, ServiceUsuario>();
        }
    }
}
