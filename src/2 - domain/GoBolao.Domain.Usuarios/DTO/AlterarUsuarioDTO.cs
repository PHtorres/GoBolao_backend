using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Usuarios.DTO
{
    public class AlterarUsuarioDTO
    {
        public int Id { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
    }
}
