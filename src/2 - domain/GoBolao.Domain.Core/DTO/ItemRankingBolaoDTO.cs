using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class ItemRankingBolaoDTO
    {
        public int IdUsuario { get; set; }
        public string ApelidoUsuario { get; set; }
        public string NomeImagemAvatarUsuario { get; set; }
        public int Pontos { get; set; }
        public decimal QuantidadePalpites { get; set; }
    }
}
