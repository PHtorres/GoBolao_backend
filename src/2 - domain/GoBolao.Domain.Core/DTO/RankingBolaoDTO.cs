using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class RankingBolaoDTO
    {
        public RankingBolaoDTO()
        {
            Classificacao = new List<ItemRankingBolaoDTO>();
        }

        public string NomeBolao { get; set; }
        public int IdBolao { get; set; }
        public string NomeCampeonato { get; set; }
        public IEnumerable<ItemRankingBolaoDTO> Classificacao { get; set; }
    }
}
