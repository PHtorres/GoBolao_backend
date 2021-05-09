using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Core.Services;
using GoBolao.Domain.Shared.Interfaces.Service;
using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Interfaces.Service;
using GoBolao.Domain.Usuarios.Services;
using GoBolao.Infra.Criptografia.Hash;
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
            service.AddScoped<IServiceCriptografia, ServiceHash>();
            service.AddScoped<IServiceAutenticacao, ServiceAutenticacao>();
            service.AddScoped<IRepositoryCampeonato, RepositoryCampeonato>();
            service.AddScoped<IServiceCampeonato, ServiceCampeonato>();
            service.AddScoped<IRepositoryBolao, RepositoryBolao>();
            service.AddScoped<IServiceBolao, ServiceBolao>();
        }
    }
}
