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

            var palpitesAcertosPlacar = palpitesDoJogo
                            .Where(p =>
                            p.PlacarMandantePalpite == finalizarJogoDTO.PlacarMandante && p.PlacarVisitantePalpite == finalizarJogoDTO.PlacarVisitante
                            ).AsEnumerable();

            var palpitesAcertosResultado = palpitesDoJogo
                                           .Where(p =>
                                           ResultadoPorPlacares(p.PlacarMandantePalpite, p.PlacarVisitantePalpite) == ResultadoPorPlacares(finalizarJogoDTO.PlacarMandante, finalizarJogoDTO.PlacarVisitante)
                                           && !palpitesAcertosPlacar.Contains(p)
                                           ).AsEnumerable();



            foreach(var palpiteAcertosResultado in palpitesAcertosResultado)
            {
                palpiteAcertosResultado.AlterarPontos(3);
                palpiteAcertosResultado.FinalizarPalpite();
            }

            foreach (var palpiteAcertosPlacar in palpitesAcertosPlacar)
            {
                palpiteAcertosPlacar.AlterarPontos(8);
                palpiteAcertosPlacar.FinalizarPalpite();
            }

            RepositorioPalpite.AtualizarLista(palpitesAcertosResultado);
            RepositorioPalpite.AtualizarLista(palpitesAcertosPlacar);
            RepositorioPalpite.Salvar();

            jogo.AlterarPlacarMandante(finalizarJogoDTO.PlacarMandante);
            jogo.AlterarPlacarVisitante(finalizarJogoDTO.PlacarVisitante);
            jogo.AlterarStatusParaFinalizado();

            RepositorioJogo.Atualizar(jogo);
            RepositorioJogo.Salvar();

            Resposta.AdicionarConteudo(jogo);
            return Resposta;

        }

        public Resposta<IEnumerable<JogoDTO>> ObterJogos(int idUsuario)
        {
            var jogos = RepositorioJogo.ObterJogos(idUsuario);
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
            if(placarMandante > placarVisitante)
            {
                return Resultado.VitoriaMandante;
            }

            if(placarVisitante > placarMandante)
            {
                return Resultado.VitoriaVisitante;
            }

            return Resultado.Empate;
        }
    }
}
