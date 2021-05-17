using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Core.Interfaces.Rules;
using GoBolao.Domain.Shared.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Domain.Core.Rules
{
    public class RulesBolao : RulesBase, IRulesBolao
    {
        private readonly IRepositoryBolao RepositorioBolao;

        public RulesBolao(IRepositoryBolao repositorioBolao)
        {
            RepositorioBolao = repositorioBolao;
        }

        public bool AptoParaCriarBolao(CriarBolaoDTO criarBolaoDTO)
        {
            NomeDeveSerUnicoNaCriacao(criarBolaoDTO.Nome);
            return SemFalhas;
        }

        public void Dispose()
        {
            RepositorioBolao.Dispose();
            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<string> ObterFalhas()
        {
            return Falhas;
        }

        private void NomeDeveSerUnicoNaCriacao(string nome)
        {
            var boloesComMesmoNome = RepositorioBolao.ObterBoloesPeloNome(nome);

            if (boloesComMesmoNome.Any())
            {
                AdicionarFalha("Nome já em uso. Escolha outro, por favor!");
            }
        }
    }
}
