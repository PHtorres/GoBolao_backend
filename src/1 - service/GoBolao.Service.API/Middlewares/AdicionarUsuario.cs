using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Doczen.Api.Middlewares
{
    public class AdicionarUsuario
    {
        private readonly RequestDelegate next;

        public AdicionarUsuario(RequestDelegate _next)
        {
            next = _next;
        }

        public Task Invoke(HttpContext contextoHttp)
        {
            //mexendo aqui so para exemplificar
            var token = contextoHttp.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(token))
            {
                token = token.Replace("Bearer ", "");
                var geradorToken = new JwtSecurityTokenHandler();
                var jsonToken = geradorToken.ReadToken(token) as JwtSecurityToken;
                var id_usuario = jsonToken.Claims.Where(c => c.Type == "id_usuario").Select(c => c.Value).FirstOrDefault();

                var request = contextoHttp.Request;
                request.QueryString =  request.QueryString.Add("id_usuario", id_usuario);
            }

            return next(contextoHttp);
        }
    }
}
