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
    public class RulesCampeonato : RulesBase, IRulesCampeonato
    {
        private readonly IRepositoryCampeonato RepositorioCampeonato;

        public RulesCampeonato(IRepositoryCampeonato repositorioCampeonato)
        {
            RepositorioCampeonato = repositorioCampeonato;
        }

        public bool AptoParaCriarCampeonato(CriarCampeonatoDTO criarCampeonatoDTO)
        {
            NomeDeveSerUnicoNaCriacao(criarCampeonatoDTO.Nome);
            return SemFalhas;
        }

        public void Dispose()
        {
            RepositorioCampeonato.Dispose();
            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<string> ObterFalhas()
        {
            return Falhas;
        }

        private void NomeDeveSerUnicoNaCriacao(string nome)
        {
            var campeonatos = RepositorioCampeonato.ObterCampeonatosPeloNome(nome);
            if(campeonatos.Any())
            {
                AdicionarFalha("Já existe um campeonato com esse nome. Escolha outro, por favor.");
            }
        }
    }
}
