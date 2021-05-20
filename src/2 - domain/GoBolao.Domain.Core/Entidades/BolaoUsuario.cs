using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Entidades
{
    public sealed class BolaoUsuario:EntidadeBase, IEntidade
    {
        public BolaoUsuario(int idBolao, int idUsuario)
        {
            IdBolao = idBolao;
            IdUsuario = idUsuario;

            Validar();
        }

        public int IdBolao { get; private set; }
        public int IdUsuario { get; private set; }

        public void Validar()
        {
            ValidarIdBolao();
            ValidarIdUsuario();
        }

        private void ValidarIdBolao()
        {
            NaoDeveSerZeroOuMenos(IdBolao, "Id do bolao inválido.");
        }

        private void ValidarIdUsuario()
        {
            NaoDeveSerZeroOuMenos(IdUsuario, "Id do usuário inválido.");
        }
    }
}
