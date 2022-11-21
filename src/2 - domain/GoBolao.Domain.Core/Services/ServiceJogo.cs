using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Core.Interfaces.Rules;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Core.ValueObjects;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Domain.Core.Services
{
    public class ServiceJogo : IServiceJogo
    {
        private readonly IRepositoryJogo RepositorioJogo;
        private readonly IRepositoryPalpite RepositorioPalpite;
        private readonly IRulesJogo RulesJogo;
        private Resposta<Jogo> Resposta;
        private Resposta<IEnumerable<JogoDTO>> RespostaListaDTO;

        public ServiceJogo(IRepositoryJogo repositorioJogo, IRepositoryPalpite repositorioPalpite, IRulesJogo rulesJogo)
        {
            RepositorioJogo = repositorioJogo;
            Resposta = new Resposta<Jogo>();
            RespostaListaDTO = new Resposta<IEnumerable<JogoDTO>>();
            RepositorioPalpite = repositorioPalpite;
            RulesJogo = rulesJogo;
        }

        public Resposta<Jogo> CriarJogo(CriarJogoDTO criarJogoDTO)
        {
            var jogo = new Jogo(criarJogoDTO.IdCampeonato, criarJogoDTO.DataHora, criarJogoDTO.IdMandante, criarJogoDTO.IdVisitante, criarJogoDTO.Fase);
            if (jogo.Invalido)
            {
                Resposta.AdicionarNotificacao(jogo._Erros);
                return Resposta;
            }

            if (!RulesJogo.AptoParaCriar(criarJogoDTO))
            {
                Resposta.AdicionarNotificacao(RulesJogo.ObterFalhas());
                return Resposta;
            }

            RepositorioJogo.Adicionar(jogo);
            RepositorioJogo.Salvar();

            Resposta.AdicionarConteudo(jogo);
            return Resposta;
        }

        public void Dispose()
        {
            RepositorioJogo.Dispose();
            RepositorioPalpite.Dispose();
            RulesJogo.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<Jogo> FinalizarJogo(FinalizarJogoDTO finalizarJogoDTO)
        {
            if (!RulesJogo.AptoParaFinalizar(finalizarJogoDTO))
            {
                Resposta.AdicionarNotificacao(RulesJogo.ObterFalhas());
                return Resposta;
            }

            var jogo = RepositorioJogo.Obter(finalizarJogoDTO.IdJogo);

            var palpitesDoJogo = RepositorioPalpite.ObterPalpitesPorJogo(finalizarJogoDTO.IdJogo);


            foreach (var palpite in palpitesDoJogo)
            {
                palpite.AlterarPontos(0);
                var resultadoJogo = ResultadoPorPlacares(finalizarJogoDTO.PlacarMandante, finalizarJogoDTO.PlacarVisitante);
                var resultadoPalpite = ResultadoPorPlacares(palpite.PlacarMandantePalpite, palpite.PlacarVisitantePalpite);
                var acertouNumeroDeGolsDoMandante = palpite.PlacarMandantePalpite == finalizarJogoDTO.PlacarMandante;
                var acertouNumeroDeGolsDoVisitante = palpite.PlacarVisitantePalpite == finalizarJogoDTO.PlacarVisitante;
                var acertouDiferencaGolsPlacar = DiferencaGolsPlacar(palpite.PlacarMandantePalpite, palpite.PlacarVisitantePalpite) == DiferencaGolsPlacar(finalizarJogoDTO.PlacarMandante, finalizarJogoDTO.PlacarVisitante);
                var acertouResultado = resultadoPalpite == resultadoJogo;
                var jogoFoiEmpate = resultadoJogo == Resultado.Empate;

                if (jogoFoiEmpate)
                {
                    if (acertouResultado) palpite.AcrescentarPontos(10);
                    if (acertouNumeroDeGolsDoMandante && acertouNumeroDeGolsDoVisitante) palpite.AcrescentarPontos(6);
                }
                else
                {
                    if (acertouNumeroDeGolsDoMandante) palpite.AcrescentarPontos(2);
                    if (acertouNumeroDeGolsDoVisitante) palpite.AcrescentarPontos(2);
                    if (acertouDiferencaGolsPlacar) palpite.AcrescentarPontos(4);
                    if (acertouResultado) palpite.AcrescentarPontos(8);
                }

                palpite.FinalizarPalpite();
            }

            RepositorioPalpite.AtualizarLista(palpitesDoJogo);
            RepositorioPalpite.Salvar();

            jogo.AlterarPlacarMandante(finalizarJogoDTO.PlacarMandante);
            jogo.AlterarPlacarVisitante(finalizarJogoDTO.PlacarVisitante);
            jogo.AlterarStatusParaFinalizado();

            RepositorioJogo.Atualizar(jogo);
            RepositorioJogo.Salvar();

            Resposta.AdicionarConteudo(jogo);
            return Resposta;

        }

        public Resposta<IEnumerable<JogoDTO>> ObterTodosJogos(int idUsuario)
        {
            var jogos = RepositorioJogo.ObterJogos(idUsuario);
            RespostaListaDTO.AdicionarConteudo(jogos);
            return RespostaListaDTO;
        }

        public Resposta<IEnumerable<JogoDTO>> ObterJogosFuturos(int idUsuario)
        {
            var jogos = RepositorioJogo.ObterJogos(idUsuario).Where(j => j.DataHora.Date > DateTime.Now.AddDays(1));
            RespostaListaDTO.AdicionarConteudo(jogos);
            return RespostaListaDTO;
        }

        public Resposta<IEnumerable<JogoDTO>> ObterJogosDeAmanha(int idUsuario)
        {
            var jogos = RepositorioJogo.ObterJogosNaData(DateTime.Now.AddDays(1), idUsuario);
            RespostaListaDTO.AdicionarConteudo(jogos);
            return RespostaListaDTO;
        }

        public Resposta<IEnumerable<JogoDTO>> ObterJogosDeHoje(int idUsuario)
        {
            var jogos = RepositorioJogo.ObterJogosNaData(DateTime.Now, idUsuario);
            RespostaListaDTO.AdicionarConteudo(jogos);
            return RespostaListaDTO;
        }

        private Resultado ResultadoPorPlacares(int placarMandante, int placarVisitante)
        {
            if (placarMandante > placarVisitante)
            {
                return Resultado.VitoriaMandante;
            }

            if (placarVisitante > placarMandante)
            {
                return Resultado.VitoriaVisitante;
            }

            return Resultado.Empate;
        }

        private int DiferencaGolsPlacar(int placarMandante, int placarVisitante)
        {
            return placarMandante - placarVisitante;
        }
    }
}
