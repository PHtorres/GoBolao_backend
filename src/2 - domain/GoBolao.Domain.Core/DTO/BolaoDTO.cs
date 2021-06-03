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
        public string NomeImagemAvatarBolao { get; set; }
        public string NomeImagemAvatarCampeonato { get; set; }
        public bool SouCriadorBolao { get; set; }
        public bool PaticipoBolao { get; set; }
        public int QuantidadeSolicitacoesAbertas { get; set; }
    }
}
