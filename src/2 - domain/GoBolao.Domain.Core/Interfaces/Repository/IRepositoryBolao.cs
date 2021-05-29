using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Shared.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Interfaces.Repository
{
    public interface IRepositoryBolao:IRepositoryGenerico<Bolao>
    {
        IEnumerable<BolaoDTO> ObterBoloesPesquisa(string pesquisa, int idUsuario);
        BolaoDTO ObterBolaoPorId(int idBolao, int idUsuaio);
        IEnumerable<BolaoDTO> ObterBoloesUsuario(int idUsuario);
        IEnumerable<Bolao> ObterBoloesPeloNome(string nome);
        IEnumerable<ItemRankingBolaoDTO> ObterClassificacaoRankingBolao(int idBolao);
    }
}
