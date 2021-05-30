using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class PalpiteDTO
    {
		public int Id { get; set; }
		public int IdJogo { get; set; }
		public int IdUsuarioPalpite { get; set; }
		public string NomeUsuarioPalpite { get; set; }
		public string Mandante { get; set; }
		public string Visitante { get; set; }
		public string NomeImagemAvatarMandante { get; set; }
		public string NomeImagemAvatarVisitante { get; set; }
		public DateTime DataHoraPalpite { get; set; }
		public DateTime DataHoraJogo { get; set; }
		public int PlacarMandantePalpite { get; set; }
		public int PlacarVisitantePalpite { get; set; }
		public int PlacarMandanteReal { get; set; }
		public int PlacarVisitanteReal { get; set; }
		public int Pontos { get; set; }
		public bool Finalizado { get; set; }
	}
}
