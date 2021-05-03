using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doczen.Api.Middlewares
{
    public static class AdicionarUsuarioExtencao
    {
        public static IApplicationBuilder UseAdicionarUsuario(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdicionarUsuario>();
        }
    }
}
