using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class BolaoDTO
    {
        public int IdBolao { get; set; }
        public string Nome { get; set; }
        public string NomeCriador { get; set; }
        public string NomeCampeonato { get; set; }
        public string Privacidade { get; set; }
        public string NomeImagemAvatar { get; set; }
        public bool SouCriadorBolao { get; set; }
    }
}
