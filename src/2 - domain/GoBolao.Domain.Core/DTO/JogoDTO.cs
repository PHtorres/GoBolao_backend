using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class JogoDTO
    {
		public string NomeCampeonato { get; set; }
		public DateTime DataHora { get; set; }
		public string TimeMandante { get; set; }
		public string TimeVisitante { get; set; }
		public int PlacarMandante { get; set; }
		public int PlacarVisitante { get; set; }
		public string Fase { get; set; }
		public bool UsuarioTemPalpite { get; set; }
	}
}
