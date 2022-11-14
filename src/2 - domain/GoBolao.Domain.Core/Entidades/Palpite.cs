using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;
using System;

namespace GoBolao.Domain.Core.Entidades
{
    public sealed class Palpite : EntidadeBase, IEntidade
    {
        public Palpite(int idJogo, int idUsuario, int placarMandantePalpite, int placarVisitantePalpite)
        {
            IdJogo = idJogo;
            IdUsuario = idUsuario;
            DataHora = DateTime.Now;
            PlacarMandantePalpite = placarMandantePalpite;
            PlacarVisitantePalpite = placarVisitantePalpite;
            Pontos = 0;
            Finalizado = false;
        }

        public int IdJogo { get; private set; }
        public int IdUsuario { get; private set; }
        public DateTime DataHora { get; private set; }
        public int PlacarMandantePalpite { get; private set; }
        public int PlacarVisitantePalpite { get; private set; }
        public int Pontos { get; private set; }
        public bool Finalizado { get; private set; }

        public void FinalizarPalpite()
        {
            Finalizado = true;
        }

        public void AlterarPontos(int pontos)
        {
            Pontos = pontos;
        }

        public void AcrescentarPontos(int pontos)
        {
            Pontos += pontos;
        }

        public void Validar()
        {
            ValidarIdJogo();
            ValidarIdUsuario();
            ValidarPlacarMandantePalpite();
            ValidarPlacarVisitantePalpite();
        }

        private void ValidarIdJogo()
        {
            NaoDeveSerZeroOuMenos(IdJogo, "Id do jogo inválido.");
        }

        private void ValidarIdUsuario()
        {
            NaoDeveSerZeroOuMenos(IdUsuario, "Id do usuário inválido.");
        }

        private void ValidarPlacarMandantePalpite()
        {
            NaoDeveSerMenorQue(0, PlacarMandantePalpite, "Placar do mandante inválido.");
        }

        private void ValidarPlacarVisitantePalpite()
        {
            NaoDeveSerMenorQue(0, PlacarVisitantePalpite, "Placar do visitante inválido.");
        }
    }
}
