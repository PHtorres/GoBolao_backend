using GoBolao.Domain.Core.DTO;
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
        Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesPorUsuario(int idUsuario);
    }
}
