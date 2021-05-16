using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class PalpiteDTO
    {
		public int Id { get; set; }
		public string Mandante { get; set; }
		public string Visitante { get; set; }
		public string NomeImagemAvatarMandante { get; set; }
		public string NomeImagemAvatarVisitante { get; set; }
		public DateTime DataHora { get; set; }
		public int PlacarMandantePalpite { get; set; }
		public int PlacarVisitantePalpite { get; set; }
		public int PlacarMandanteReal { get; set; }
		public int PlacarVisitanteReal { get; set; }
		public int Pontos { get; set; }
		public bool Finalizado { get; set; }
	}
}
