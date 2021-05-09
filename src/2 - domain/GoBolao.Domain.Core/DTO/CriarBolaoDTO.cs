using GoBolao.Domain.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class CriarBolaoDTO
    {
        public string Nome { get; set; }
        public int IdCampeonato { get; set; }
        public Privacidade Privacidade { get; set; }
    }
}
