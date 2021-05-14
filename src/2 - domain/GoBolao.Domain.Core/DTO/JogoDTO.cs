using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.DTO
{
    public class JogoDTO
    {
		public int Id { get; set; }
		public string NomeCampeonato { get; set; }
		public DateTime DataHora { get; set; }
		public string Mandante { get; set; }
		public string Visitante { get; set; }
		public string NomeImagemAvatarMandante { get; set; }
		public string NomeImagemAvatarVisitante { get; set; }
		public int PlacarMandante { get; set; }
		public int PlacarVisitante { get; set; }
		public string Fase { get; set; }
		public bool UsuarioTemPalpite { get; set; }
	}
}
