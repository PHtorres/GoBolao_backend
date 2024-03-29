﻿using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Service
{
    public interface IServicePalpite:IDisposable
    {
        Resposta<Palpite> CriarPalpite(CriarPalpiteDTO criarPalpiteDTO, int idUsuarioAcao);
        Resposta<Palpite> RemoverPalpite(int idPalpite, int idUsuarioAcao);
        Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesAbertosPorUsuario(int idUsuarioAcao);
        Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesFinalizadosPorUsuario(int idUsuarioAcao);
        Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesFinalizadosPorUsuarioDeUmBolao(int idUsuario, int idBolao);
        Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesPorJogoFinalizadoOuIniciadoDosAdiversarios(int idJogo, int idUsuarioAcao);
    }
}
