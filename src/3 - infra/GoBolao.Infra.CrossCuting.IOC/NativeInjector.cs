using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Core.Interfaces.Rules;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Core.Rules;
using GoBolao.Domain.Core.Services;
using GoBolao.Domain.Shared.Interfaces.Service;
using GoBolao.Domain.Usuarios.Interfaces.Repository;
using GoBolao.Domain.Usuarios.Interfaces.Rules;
using GoBolao.Domain.Usuarios.Interfaces.Service;
using GoBolao.Domain.Usuarios.Rules;
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
            service.AddScoped<IRulesUsuario, RulesUsuario>();
            service.AddScoped<IServiceUsuario, ServiceUsuario>();

            service.AddScoped<IServiceCriptografia, ServiceHash>();

            service.AddScoped<IServiceAutenticacao, ServiceAutenticacao>();

            service.AddScoped<IRepositoryTime, RepositoryTime>();
            service.AddScoped<IRulesTime, RulesTime>();
            service.AddScoped<IServiceTime, ServiceTime>();

            service.AddScoped<IRepositoryCampeonato, RepositoryCampeonato>();
            service.AddScoped<IRulesCampeonato, RulesCampeonato>();
            service.AddScoped<IServiceCampeonato, ServiceCampeonato>();

            service.AddScoped<IRepositoryBolao, RepositoryBolao>();
            service.AddScoped<IRulesBolao, RulesBolao>();
            service.AddScoped<IServiceBolao, ServiceBolao>();

            service.AddScoped<IRepositoryJogo, RepositoryJogo>();
            service.AddScoped<IRulesJogo, RulesJogo>();
            service.AddScoped<IServiceJogo, ServiceJogo>();

            service.AddScoped<IRepositoryPalpite, RepositoryPalpite>();
            service.AddScoped<IServicePalpite, ServicePalpite>();
        }
    }
}
