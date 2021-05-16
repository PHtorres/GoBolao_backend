using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class FinalizarJogoDTO
    {
        public int IdJogo { get; set; }
        public int PlacarMandante { get; set; }
        public int PlacarVisitante { get; set; }
    }
}
