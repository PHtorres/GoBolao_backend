using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class BolaoSolicitacaoDTO
    {
        public int IdSolicitacao { get; set; }
        public int IdBolao { get; set; }
        public string ApelidoUsuarioSolicitante { get; set; }
        public string NomeBolao { get; set; }
        public string Status { get; set; }
    }
}
