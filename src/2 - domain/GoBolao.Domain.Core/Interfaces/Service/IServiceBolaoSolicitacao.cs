using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Service
{
    public interface IServiceBolaoSolicitacao:IDisposable
    {
        Resposta<BolaoSolicitacao> CriarSolicitacao(CriarBolaoSolicitacaoDTO criarBolaoSolicitacaoDTO, int idUsuarioAcao);
        Resposta<BolaoSolicitacao> DesfazerSolicitacao(int idSolicitacao, int idUsuarioAcao);
        Resposta<BolaoSolicitacao> AceitarSolicitacao(AceitarBolaoSolicitacaoDTO aceitarBolaoSolicitacaoDTO, int idUsuarioAcao);
        Resposta<BolaoSolicitacao> RecusarSolicitacao(RecusarBolaoSolicitacaoDTO recusarBolaoSolicitacaoDTO, int idUsuarioAcao);
        Resposta<IEnumerable<BolaoSolicitacaoDTO>> ObterSolicitacoesPorBolao(int idBolao, int idUsuarioAcao);
    }
}
