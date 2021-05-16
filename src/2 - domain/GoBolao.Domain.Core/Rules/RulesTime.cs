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
    public abstract class RulesTime : RulesBase, IRulesTime
    {
        private readonly IRepositoryTime RepositorioTime;

        public RulesTime(IRepositoryTime repositorioTime)
        {
            RepositorioTime = repositorioTime;
        }

        public bool AptoParaAlterar(AlterarTimeDTO alterarTimeDTO)
        {
            NomeDeveSerUnicoAoAlterar(alterarTimeDTO.Nome, alterarTimeDTO.Id);
            return SemFalhas;
        }

        public bool AptoParaCriar(CriarTimeDTO criarTimeDTO)
        {
            NomeDeveSerUnicoAoCriar(criarTimeDTO.Nome);
            return SemFalhas;
        }

        public void Dispose()
        {
            RepositorioTime.Dispose();
            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<string> ObterFalhas()
        {
            return Falhas;
        }

        private void NomeDeveSerUnicoAoCriar(string nome)
        {
            var outrosTimesComMesmoNome = RepositorioTime.ObterTimesPeloNome(nome);
            if (outrosTimesComMesmoNome.Any())
            {
                AdicionarFalha("Nome já em uso. Escolha outro, por favor.");
            }
        }

        private void NomeDeveSerUnicoAoAlterar(string nome, int idTime)
        {
            var outrosTimesComMesmoNome = RepositorioTime.ObterTimesPeloNome(nome).Where(t => t.Id != idTime);
            if (outrosTimesComMesmoNome.Any())
            {
                AdicionarFalha("Nome já em uso. Escolha outro, por favor.");
            }
        }
    }
}
