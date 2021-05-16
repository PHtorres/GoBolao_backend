using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.DomainObjects;
using GoBolao.Domain.Shared.Interfaces.Repository;
using System;
using System.Collections.Generic;

namespace GoBolao.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryTime : IRepositoryGenerico<Time>
    {
        IEnumerable<Time> ObterTimesPeloNome(string nome);
    }
}
