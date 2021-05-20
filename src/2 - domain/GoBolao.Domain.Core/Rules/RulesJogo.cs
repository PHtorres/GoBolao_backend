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
    public class RulesJogo : RulesBase, IRulesJogo
    {
        private readonly IRepositoryJogo RepositorioJogo;
        private readonly IRepositoryTime RepositorioTime;
        private readonly IRepositoryCampeonato RepositorioCampeonato;

        public RulesJogo(IRepositoryJogo repositorioJogo, IRepositoryTime repositorioTime, IRepositoryCampeonato repositorioCampeonato)
        {
            RepositorioJogo = repositorioJogo;
            RepositorioTime = repositorioTime;
            RepositorioCampeonato = repositorioCampeonato;
        }

        public bool AptoParaCriar(CriarJogoDTO criarJogoDTO)
        {
            JogoDeveSerUnicoNaData(criarJogoDTO.DataHora, criarJogoDTO.IdMandante, criarJogoDTO.IdVisitante);
            TimeMandanteDeveSerDiferenteDoTimeVisitante(criarJogoDTO.IdMandante, criarJogoDTO.IdVisitante);
            TimeMandanteDeveExistir(criarJogoDTO.IdMandante);
            TimeVisitanteDeveExistir(criarJogoDTO.IdVisitante);
            CampeonatoDeveExistir(criarJogoDTO.IdCampeonato);

            return SemFalhas;
        }

        public bool AptoParaFinalizar(FinalizarJogoDTO finalizarJogoDTO)
        {
            JogoDeveExistir(finalizarJogoDTO.IdJogo);
            JogoNaoDeveEstarFinalizado(finalizarJogoDTO.IdJogo);

            return SemFalhas;
        }

        public void Dispose()
        {
            RepositorioJogo.Dispose();
            RepositorioTime.Dispose();
            RepositorioCampeonato.Dispose();
            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<string> ObterFalhas()
        {
            return Falhas;
        }

        private void JogoDeveSerUnicoNaData(DateTime data, int idMandante, int idVisitante)
        {
            var jogosNaData = RepositorioJogo.ObterJogosNaData(data);
            var jogosNaDataDoMesmoTimeMandante = jogosNaData.Where(j => j.IdMandante == idMandante || j.IdVisitante == idMandante);
            var jogosNaDataDoMesmoTimeVisitante = jogosNaData.Where(j => j.IdMandante == idVisitante || j.IdVisitante == idVisitante);

            if (jogosNaDataDoMesmoTimeMandante.Any())
            {
                AdicionarFalha("Já existe outro jogo do time mandante nesta data. Escolha outra data, por favor.");
            }

            if (jogosNaDataDoMesmoTimeVisitante.Any())
            {
                AdicionarFalha("Já existe outro jogo do time visitante nesta data. Escolha outra data, por favor.");
            }
        }

        private void TimeMandanteDeveSerDiferenteDoTimeVisitante(int idMandante, int idVisitante)
        {
            if(idMandante == idVisitante)
            {
                AdicionarFalha("Time mandante deve ser diferente do time visitante.");
            }
        }

        private void TimeMandanteDeveExistir(int idMandante)
        {
            var timeMandante = RepositorioTime.Obter(idMandante);
            if(timeMandante == null)
            {
                AdicionarFalha("Time mandante não existe.");
            }
        }

        private void TimeVisitanteDeveExistir(int idVisitante)
        {
            var timeVisitante = RepositorioTime.Obter(idVisitante);
            if (timeVisitante == null)
            {
                AdicionarFalha("Time visitante não existe.");
            }
        }

        private void CampeonatoDeveExistir(int idCampeonato)
        {
            var campeonato = RepositorioCampeonato.Obter(idCampeonato);
            if(campeonato == null)
            {
                AdicionarFalha("Campeonato não existe.");
            }
        }

        private void JogoDeveExistir(int idJogo)
        {
            var jogo = RepositorioJogo.Obter(idJogo);
            if(jogo == null)
            {
                AdicionarFalha("Jogo não existe.");
            }
        }

        private void JogoNaoDeveEstarFinalizado(int idJogo)
        {
            var jogo = RepositorioJogo.Obter(idJogo);
            if (jogo.Finalizado)
            {
                AdicionarFalha("Jogo já finalizado antes.");
            }
        }
    }
}
