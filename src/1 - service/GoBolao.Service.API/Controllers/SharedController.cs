using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBolao.Service.API.Controllers
{
    [ApiController]
    public class SharedController : ControllerBase
    {
        protected int IdUsuarioAcao;
        protected IHttpContextAccessor httpContextAccessor;

        public SharedController(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;

            try
            {
                IdUsuarioAcao = Convert.ToInt32(httpContextAccessor.HttpContext.Request.Query["id_usuario"][0]);
            }
            catch
            {
                IdUsuarioAcao = 0;
            }

        }
    }
}
