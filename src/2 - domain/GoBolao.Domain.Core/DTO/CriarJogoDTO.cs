using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class CriarJogoDTO
    {
		public int IdCampeonato { get; set; }
		public DateTime DataHora { get; set; }
		public int IdMandante { get; set; }
		public int IdVisitante { get; set; }
		public string Fase { get; set; }
	}
}
