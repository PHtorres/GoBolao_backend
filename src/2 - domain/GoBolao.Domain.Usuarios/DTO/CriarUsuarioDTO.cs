using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Usuarios.DTO
{
    public class CriarUsuarioDTO
    {
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmaSenha { get; set; }
    }
}
