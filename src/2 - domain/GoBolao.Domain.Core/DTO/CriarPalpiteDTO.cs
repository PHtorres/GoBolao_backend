using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class CriarPalpiteDTO
    {
		public int IdJogo { get; set; }
		public int PlacarMandantePalpite { get; set; }
		public int PlacarVisitantePalpite { get; set; }
	}
}
