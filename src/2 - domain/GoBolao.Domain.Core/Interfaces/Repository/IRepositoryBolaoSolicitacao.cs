﻿using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryBolaoSolicitacao:IRepositoryGenerico<BolaoSolicitacao>
    {
        IEnumerable<BolaoSolicitacaoDTO> ObterSolicitacoesPorBolao(int idBolao);
    }
}
