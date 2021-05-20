using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Shared.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Rules
{
    public interface IRulesBolaoSolicitacao:IRules
    {
        bool AptoParaAceitarSolicitacao(AceitarBolaoSolicitacaoDTO aceitarBolaoSolicitacaoDTO, int idUsuarioAcao);
        bool AptoParaCriarSolicitacao(CriarBolaoSolicitacaoDTO criarBolaoSolicitacaoDTO, int idUsuarioAcao);
        bool AptoParaDesfazerSolicitacao(int idSolicitacao, int idUsuarioAcao);
        bool AptoPareRecusarSolicitacao(RecusarBolaoSolicitacaoDTO recusarBolaoSolicitacaoDTO, int idUsuarioAcao);
        bool AptoParaObterSolicitacoes(int idBolao, int idUsuarioAcao);
    }
}
