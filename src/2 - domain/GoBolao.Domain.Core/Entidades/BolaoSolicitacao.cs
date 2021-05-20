using GoBolao.Domain.Core.ValueObjects;
using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Entidades
{
    public sealed class BolaoSolicitacao:EntidadeBase, IEntidade
    {
        public BolaoSolicitacao(int idBolao, int idUsuarioSolicitante)
        {
            IdBolao = idBolao;
            IdUsuarioSolicitante = idUsuarioSolicitante;
            Status = StatusBolaoSolicitacao.Aberta;

            Validar();
        }

        public int IdBolao { get; private set; }
        public int IdUsuarioSolicitante { get; private set; }
        public StatusBolaoSolicitacao Status { get; private set; }

        public void AceitarSolicitacao()
        {
            Status = StatusBolaoSolicitacao.Aceita;
        }

        public void RecusarSolicitacao()
        {
            Status = StatusBolaoSolicitacao.Recusada;
        }

        public void Validar()
        {
            ValidarIdBolao();
            ValidarIdUsuarioSolicitante();
        }

        private void ValidarIdBolao()
        {
            NaoDeveSerZeroOuMenos(IdBolao, "Id do bolao inválido.");
        }

        private void ValidarIdUsuarioSolicitante()
        {
            NaoDeveSerZeroOuMenos(IdUsuarioSolicitante, "Id do usuário solicitante inválido.");
        }
    }
}
