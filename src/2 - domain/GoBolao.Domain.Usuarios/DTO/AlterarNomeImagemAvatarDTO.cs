using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Usuarios.DTO
{
    public class AlterarNomeImagemAvatarDTO
    {
        public int IdUsuario { get; set; }
        public string NomeImagemAvatar { get; set; }
    }
}
