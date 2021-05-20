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
    public class RulesPalpite : RulesBase, IRulesPalpite
    {
        private readonly IRepositoryPalpite RepositorioPalpite;
        private readonly IRepositoryJogo RepositorioJogo;

        public RulesPalpite(IRepositoryPalpite repositorioPalpite, IRepositoryJogo repositorioJogo)
        {
            RepositorioPalpite = repositorioPalpite;
            RepositorioJogo = repositorioJogo;
        }

        public bool AptoParaCriar(CriarPalpiteDTO criarPalpiteDTO, int idUsuarioAcao)
        {
            JogoDeveExistir(criarPalpiteDTO.IdJogo);
            PalpiteDeveSerAntesDoJogoIniciar(criarPalpiteDTO.IdJogo);
            PalpiteNoJogoDeveSerUnicoParaOUsuario(criarPalpiteDTO.IdJogo, idUsuarioAcao);
            JogoNaoDeveEstarFinalizado(criarPalpiteDTO.IdJogo);
            return SemFalhas;
        }

        public bool AptoParaRemover(int idPalpite, int idUsuarioAcao)
        {
            PalpiteDeveExistir(idPalpite);
            UsuarioAcaoDeveSerUsuarioDoPalpiete(idPalpite, idUsuarioAcao);
            RemoverPalpiteDeveSerAntesDoJogoIniciar(idPalpite);
            return SemFalhas;
        }

        public void Dispose()
        {
            RepositorioPalpite.Dispose();
            RepositorioJogo.Dispose();
            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<string> ObterFalhas()
        {
            throw new NotImplementedException();
        }

        private void JogoDeveExistir(int idJogo)
        {
            var jogo = RepositorioJogo.Obter(idJogo);
            if(jogo == null)
            {
                AdicionarFalha("Jogo não existe.");
            }
        }

        private void PalpiteDeveSerAntesDoJogoIniciar(int idJogo)
        {
            var jogo = RepositorioJogo.Obter(idJogo);
            if(jogo.DataHora < DateTime.Now.AddMinutes(2))
            {
                AdicionarFalha("Horário expirado. Não é mais possível criar palpites.");
            }
        }

        private void PalpiteNoJogoDeveSerUnicoParaOUsuario(int idJogo, int idUsuario)
        {
            var outrosPalpitesUsuarioNoJogo = RepositorioPalpite.ObterPalpitesPorUsuario(idUsuario).Where(p => p.IdJogo == idJogo);
            if (outrosPalpitesUsuarioNoJogo.Any())
            {
                AdicionarFalha("Você já tem palpite nesse jogo.");
            }
        }

        private void JogoNaoDeveEstarFinalizado(int idJogo)
        {
            var jogo = RepositorioJogo.Obter(idJogo);
            if (jogo.Finalizado)
            {
                AdicionarFalha("Jogo já finalizado. Palpites encerrados.");
            }
        }

        private void UsuarioAcaoDeveSerUsuarioDoPalpiete(int idPalpite, int idUsuarioAcao)
        {
            var palpite = RepositorioPalpite.Obter(idPalpite);
            if(palpite != null)
            {
                if(palpite.IdUsuario != idUsuarioAcao)
                {
                    AdicionarFalha("Palpite só deve ser removido pelo usuário que o criou.");
                }
            }
        }

        private void PalpiteDeveExistir(int idPalpite)
        {
            var palpite = RepositorioPalpite.Obter(idPalpite);
            if(palpite == null)
            {
                AdicionarFalha("Palpite não existe.");
            }
        }

        private void RemoverPalpiteDeveSerAntesDoJogoIniciar(int idPalpite)
        {
            var palpite = RepositorioPalpite.Obter(idPalpite);
            if(palpite != null)
            {
                var jogo = RepositorioJogo.Obter(palpite.IdJogo);
                if (jogo.DataHora < DateTime.Now.AddMinutes(2))
                {
                    AdicionarFalha("Horário expirado. Não é mais possível remover palpites.");
                }
            }
        }
    }
}
