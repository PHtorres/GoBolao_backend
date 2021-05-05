using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;

namespace GoBolao.Domain.Core.Interfaces.Service
{
    public interface IServiceCampeonato:IDisposable
    {
        Resposta<Campeonato> CriarCampeonato(CriarCampeonatoDTO criarcampeonatodto);
        Resposta<Campeonato> AlterarUrlAvatar(AlterarUrlAvatarCampeonatoDTO alterarUrlAvatarCampeonatoDTO);
        Resposta<IEnumerable<Campeonato>> ObterCampeonatos();
    }
}
