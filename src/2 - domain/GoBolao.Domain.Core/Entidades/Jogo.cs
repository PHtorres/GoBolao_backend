using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Entidades
{
    public sealed class Jogo:EntidadeBase, IEntidade
    {
        public Jogo(int idCampeonato, DateTime dataHora, int idMandante, int idVisitante, string fase)
        {
            IdCampeonato = idCampeonato;
            DataHora = dataHora;
            IdMandante = idMandante;
            IdVisitante = idVisitante;
            Fase = fase;

            Validar();
        }

        public int IdCampeonato { get; private set; }
		public DateTime DataHora { get; private set; }
		public int IdMandante { get; private set; }
		public int IdVisitante { get; private set; }
		public int PlacarMandante { get; private set; }
		public int PlacarVisitante { get; private set; }
		public string Fase { get; private set; }
        public bool Finalizado { get; private set; }

        public void AlterarPlacarMandante(int placarMandante)
        {
            PlacarMandante = placarMandante;
            ValidarPlacarMandante();
        }

        public void AlterarPlacarVisitante(int placarVisitante)
        {
            PlacarVisitante = placarVisitante;
            ValidarPlacarVisitante();
        }

        public void AlterarStatusParaFinalizado()
        {
            Finalizado = true;
        }

        public void Validar()
        {
            ValidarIdCampeonato();
            ValidarDataHora();
            ValidarIdMandante();
            ValidarIdVisitante();
            ValidarFase();
        }

        private void ValidarIdCampeonato()
        {
            NaoDeveSerZeroOuMenos(IdCampeonato, "Id do campeonato inválido");
        }

        private void ValidarDataHora()
        {
            NaoDeveSerMenorQue(DateTime.Now, DataHora, "Data e hora do jogo devem ser futuras.");
            NaoDeveSerNulo(DataHora, "Data e hora inválida.");
        }

        private void ValidarIdMandante()
        {
            NaoDeveSerZeroOuMenos(IdMandante, "Id do mandante inválido");
        }

        private void ValidarIdVisitante()
        {
            NaoDeveSerZeroOuMenos(IdVisitante, "Id do visitante inválido");
        }

        private void ValidarFase()
        {
            NaoDeveSerVazio(Fase, "Fase precisa ser informada.");
        }

        private void ValidarPlacarMandante()
        {
            NaoDeveSerMenorQue(0, PlacarMandante, "Placar do mandante inválido.");
            NaoDeveSerMaiorQue(90, PlacarMandante, "Placar do mandante fictício.");
        }

        private void ValidarPlacarVisitante()
        {
            NaoDeveSerMenorQue(0, PlacarVisitante, "Placar do visitante inválido.");
            NaoDeveSerMaiorQue(90, PlacarVisitante, "Placar do visitante fictício.");
        }
    }
}
