using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Service
{
    public interface IServiceTime:IDisposable
    {
        Resposta<Time> CriarTime(CriarTimeDTO criarTimeDTO);
        Resposta<Time> AlterarTime(AlterarTimeDTO alterarTimeDTO);
    }
}
