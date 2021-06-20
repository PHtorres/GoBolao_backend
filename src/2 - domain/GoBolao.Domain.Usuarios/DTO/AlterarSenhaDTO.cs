using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Usuarios.DTO
{
    public class AlterarSenhaDTO
    {
        public string SenhaAtual { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmaSenha { get; set; }
    }
}
