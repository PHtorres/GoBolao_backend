using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Core.Interfaces.Rules;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Domain.Core.Services
{
    public class ServiceBolao : IServiceBolao
    {
        private readonly IRepositoryBolao RepositorioBolao;
        private readonly IRepositoryBolaoUsuario RepositorioBolaoUsuario;
        private readonly IRepositoryCampeonato RepositorioCampeonato;
        private readonly IRulesBolao RulesBolao;
        private Resposta<Bolao> Resposta;
        private Resposta<BolaoUsuario> RespostaBolaoUsuario;
        private Resposta<BolaoDTO> RespostaDTO;
        private Resposta<IEnumerable<BolaoDTO>> RespostaListaDTO;
        private Resposta<RankingBolaoDTO> RespostaRanking;
        private Resposta<IEnumerable<RankingBolaoDTO>> RespostaListaRanking;

        public ServiceBolao(IRepositoryBolao repositorioBolao, IRulesBolao rulesBolao, IRepositoryBolaoUsuario repositorioBolaoUsuario, IRepositoryPalpite repositorioPalpite, IRepositoryCampeonato repositorioCampeonato)
        {
            RepositorioBolao = repositorioBolao;
            Resposta = new Resposta<Bolao>();
            RespostaBolaoUsuario = new Resposta<BolaoUsuario>();
            RespostaDTO = new Resposta<BolaoDTO>();
            RespostaListaDTO = new Resposta<IEnumerable<BolaoDTO>>();
            RespostaRanking = new Resposta<RankingBolaoDTO>();
            RespostaListaRanking = new Resposta<IEnumerable<RankingBolaoDTO>>();
            RulesBolao = rulesBolao;
            RepositorioBolaoUsuario = repositorioBolaoUsuario;
            RepositorioCampeonato = repositorioCampeonato;
        }

        public void Dispose()
        {
            RepositorioBolao.Dispose();
            RepositorioBolaoUsuario.Dispose();
            RulesBolao.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<Bolao> AlterarNomeImagemAvatar(AlterarNomeImagemAvatarBolaoDTO alterarNomeImagemAvatarBolaoDTO, int idUsuarioAcao)
        {
            var bolao = RepositorioBolao.Obter(alterarNomeImagemAvatarBolaoDTO.IdBolao);
            bolao.AlterarNomeImagemAvatar(alterarNomeImagemAvatarBolaoDTO.NomeImagemAvatar);

            if (bolao.Invalido)
            {
                Resposta.AdicionarNotificacao(bolao._Erros);
                return Resposta;
            }

            RepositorioBolao.Atualizar(bolao);
            RepositorioBolao.Salvar();

            Resposta.AdicionarConteudo(bolao);
            return Resposta;
        }

        public Resposta<Bolao> CriarBolao(CriarBolaoDTO criarBolaoDTO, int idUsuarioAcao)
        {
            var bolao = new Bolao(criarBolaoDTO.Nome, idUsuarioAcao, criarBolaoDTO.IdCampeonato, criarBolaoDTO.Privacidade);

            if (bolao.Invalido)
            {
                Resposta.AdicionarNotificacao(bolao._Erros);
                return Resposta;
            }

            if (!RulesBolao.AptoParaCriarBolao(criarBolaoDTO))
            {
                Resposta.AdicionarNotificacao(RulesBolao.ObterFalhas());
                return Resposta;
            }

            RepositorioBolao.Adicionar(bolao);
            RepositorioBolao.Salvar();

            var bolaoUsuario = new BolaoUsuario(bolao.Id, idUsuarioAcao);
            RepositorioBolaoUsuario.Adicionar(bolaoUsuario);
            RepositorioBolaoUsuario.Salvar();

            Resposta.AdicionarConteudo(bolao);
            return Resposta;
        }


        public Resposta<BolaoDTO> ObterBolaoPorId(int idBolao, int idUsuarioAcao)
        {
            var bolaoDTO = RepositorioBolao.ObterBolaoPorId(idBolao, idUsuarioAcao);
            if (bolaoDTO == null)
            {
                RespostaDTO.AdicionarNotificacao("Bolao nao encontrado.");
                return RespostaDTO;
            }

            bolaoDTO.SouCriadorBolao = RepositorioBolao.Obter(idBolao).IdCriador == idUsuarioAcao;

            RespostaDTO.AdicionarConteudo(bolaoDTO);
            return RespostaDTO;
        }

        public Resposta<IEnumerable<BolaoDTO>> ObterBoloesDoUsuario(int idUsuarioAcao)
        {
            RespostaListaDTO.AdicionarConteudo(RepositorioBolao.ObterBoloesUsuario(idUsuarioAcao));
            return RespostaListaDTO;
        }

        public Resposta<RankingBolaoDTO> ObterRankingBolao(int idBolao)
        {
            RespostaRanking.AdicionarConteudo(GerarRankingBolao(idBolao));
            return RespostaRanking;
        }

        private RankingBolaoDTO GerarRankingBolao(int idBolao)
        {
            var bolao = RepositorioBolao.Obter(idBolao);
            var campeonato = RepositorioCampeonato.Obter(bolao.IdCampeonato);

            var rankingBolao = new RankingBolaoDTO
            {
                Classificacao = RepositorioBolao.ObterClassificacaoRankingBolao(idBolao),
                IdBolao = idBolao,
                NomeBolao = bolao.Nome,
                NomeCampeonato = campeonato.Nome
            };

            return rankingBolao;
        }

        public Resposta<BolaoUsuario> ParticiparDeBolaoPublico(ParticiparDeBolaoPublicoDTO participarDeBolaoPublicoDTO, int idUsuarioAcao)
        {
            var bolaoUsuario = new BolaoUsuario(participarDeBolaoPublicoDTO.IdBolao, idUsuarioAcao);

            if (bolaoUsuario.Invalido)
            {
                Resposta.AdicionarNotificacao(bolaoUsuario._Erros);
                return RespostaBolaoUsuario;
            }

            if (!RulesBolao.AptoParaParticiparDeBolaoPublico(participarDeBolaoPublicoDTO, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesBolao.ObterFalhas());
                return RespostaBolaoUsuario;
            }

            RepositorioBolaoUsuario.Adicionar(bolaoUsuario);
            RepositorioBolao.Salvar();

            RespostaBolaoUsuario.AdicionarConteudo(bolaoUsuario);
            return RespostaBolaoUsuario;
        }

        public Resposta<IEnumerable<BolaoDTO>> PesquisarBoloes(string pesquisa, int IdUsuarioAcao)
        {
            var resultadoPesquisa = RepositorioBolao.ObterBoloesPesquisa(pesquisa, IdUsuarioAcao);
            RespostaListaDTO.AdicionarConteudo(resultadoPesquisa);
            return RespostaListaDTO;
        }

        public Resposta<BolaoUsuario> SairDeBolao(int idBolao, int idUsuarioAcao)
        {
            if (!RulesBolao.AptoParaSairDoBolao(idBolao, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesBolao.ObterFalhas());
                return RespostaBolaoUsuario;
            }

            var bolaoUsuario = RepositorioBolaoUsuario.ObterUsuariosDoBolao(idBolao).Where(bu => bu.IdUsuario == idUsuarioAcao).FirstOrDefault();

            RepositorioBolaoUsuario.Remover(bolaoUsuario);
            RepositorioBolaoUsuario.Salvar();

            RespostaBolaoUsuario.AdicionarConteudo(bolaoUsuario);
            return RespostaBolaoUsuario;
        }

        public Resposta<IEnumerable<RankingBolaoDTO>> ObterRankingsBoloesDoUsuario(int idUsuarioAcao)
        {
            var boloesUsuario = RepositorioBolaoUsuario.Listar().Where(bu => bu.IdUsuario == idUsuarioAcao);
            var listaRankings = new List<RankingBolaoDTO>();
            foreach(var bu in boloesUsuario)
            {
                listaRankings.Add(GerarRankingBolao(bu.IdBolao));
            }

            RespostaListaRanking.AdicionarConteudo(listaRankings);
            return RespostaListaRanking;
        }
    }
}
