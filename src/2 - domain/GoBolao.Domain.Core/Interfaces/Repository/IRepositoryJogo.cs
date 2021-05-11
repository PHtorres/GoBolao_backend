using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Shared.Interfaces.Repository;
using System;
using System.Collections.Generic;

namespace GoBolao.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryJogo : IRepositoryGenerico<Jogo>
    {
        IEnumerable<JogoDTO> ObterJogosNaData(DateTime data, int idUsuario);
        IEnumerable<JogoDTO> ObterJogos(int idUsuario);
    }
}
