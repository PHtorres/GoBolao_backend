using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Shared.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Rules
{
    public interface IRulesPalpite:IRules
    {
        bool AptoParaCriar(CriarPalpiteDTO criarPalpiteDTO, int idUsuaioAcao);
        bool AptoParaRemover(int idPalpite, int idUsuarioAcao);
    }
}
