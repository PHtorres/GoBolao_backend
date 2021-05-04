using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Usuarios.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBolao.Service.API.Models
{
    public class RespostaAutenticacao
    {
        public string Token { get; set; }
        public Resposta<UsuarioDTO> Resposta { get; set; }
    }
}
