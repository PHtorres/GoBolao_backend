using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Shared.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Rules
{
    public interface IRulesBolao:IRules
    {
        bool AptoParaCriarBolao(CriarBolaoDTO criarBolaoDTO);
        bool AptoParaParticiparDeBolaoPublico(ParticiparDeBolaoPublicoDTO participarDeBolaoPublicoDTO, int idUsuarioAcao);
        bool AptoParaSairDoBolao(int idBolao, int idUsuarioAcao);
    }
}
