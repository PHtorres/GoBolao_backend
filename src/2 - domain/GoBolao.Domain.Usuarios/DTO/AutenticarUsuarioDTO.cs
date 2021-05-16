using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Usuarios.DTO
{
    public class AutenticarUsuarioDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
