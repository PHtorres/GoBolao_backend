using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Core.Interfaces.Rules;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Services
{
    public class ServiceBolaoSolicitacao : IServiceBolaoSolicitacao
    {
        private readonly IRepositoryBolaoSolicitacao RepositorioBolaoSolicitacao;
        private readonly IRepositoryBolaoUsuario RepositorioBolaoUsuario;
        private readonly IRulesBolaoSolicitacao RulesBolaoSolicitacao;
        private Resposta<BolaoSolicitacao> Resposta;
        private Resposta<IEnumerable<BolaoSolicitacaoDTO>> RespostaListaDTO;

        public ServiceBolaoSolicitacao(IRepositoryBolaoSolicitacao repositorioBolaoSolicitacao, 
                                       IRepositoryBolaoUsuario repositorioBolaoUsuario,
                                       IRulesBolaoSolicitacao rulesBolaoSolicitacao)
        {
            RepositorioBolaoSolicitacao = repositorioBolaoSolicitacao;
            RepositorioBolaoUsuario = repositorioBolaoUsuario;
            Resposta = new Resposta<BolaoSolicitacao>();
            RulesBolaoSolicitacao = rulesBolaoSolicitacao;
        }

        public void Dispose()
        {
            RepositorioBolaoSolicitacao.Dispose();
            RepositorioBolaoUsuario.Dispose();
            RulesBolaoSolicitacao.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<BolaoSolicitacao> AceitarSolicitacao(AceitarBolaoSolicitacaoDTO aceitarBolaoSolicitacaoDTO, int idUsuarioAcao)
        {
            if(!RulesBolaoSolicitacao.AptoParaAceitarSolicitacao(aceitarBolaoSolicitacaoDTO, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesBolaoSolicitacao.ObterFalhas());
                return Resposta;
            }

            var bolaoSolicitacao = RepositorioBolaoSolicitacao.Obter(aceitarBolaoSolicitacaoDTO.IdSolicitacao);
            bolaoSolicitacao.AceitarSolicitacao();

            RepositorioBolaoSolicitacao.Atualizar(bolaoSolicitacao);
            RepositorioBolaoSolicitacao.Salvar();

            var bolaoUsuario = new BolaoUsuario(bolaoSolicitacao.IdBolao, bolaoSolicitacao.IdUsuarioSolicitante);
            RepositorioBolaoUsuario.Adicionar(bolaoUsuario);
            RepositorioBolaoUsuario.Salvar();

            Resposta.AdicionarConteudo(bolaoSolicitacao);
            return Resposta;
        }

        public Resposta<BolaoSolicitacao> CriarSolicitacao(CriarBolaoSolicitacaoDTO criarBolaoSolicitacaoDTO, int idUsuarioAcao)
        {
            var bolaoSolicitacao = new BolaoSolicitacao(criarBolaoSolicitacaoDTO.IdBolao, idUsuarioAcao);

            if (bolaoSolicitacao.Invalido)
            {
                Resposta.AdicionarNotificacao(bolaoSolicitacao._Erros);
                return Resposta;
            }

            if (!RulesBolaoSolicitacao.AptoParaCriarSolicitacao(criarBolaoSolicitacaoDTO, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesBolaoSolicitacao.ObterFalhas());
                return Resposta;
            }

            RepositorioBolaoSolicitacao.Adicionar(bolaoSolicitacao);
            RepositorioBolaoSolicitacao.Salvar();

            Resposta.AdicionarConteudo(bolaoSolicitacao);
            return Resposta;
        }

        public Resposta<BolaoSolicitacao> DesfazerSolicitacao(int idSolicitacao, int idUsuarioAcao)
        {
            if (!RulesBolaoSolicitacao.AptoParaDesfazerSolicitacao(idSolicitacao, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesBolaoSolicitacao.ObterFalhas());
                return Resposta;
            }

            var bolaoSolicitacao = RepositorioBolaoSolicitacao.Obter(idSolicitacao);

            RepositorioBolaoSolicitacao.Remover(bolaoSolicitacao);
            RepositorioBolaoSolicitacao.Salvar();

            Resposta.AdicionarConteudo(bolaoSolicitacao);
            return Resposta;
        }

        public Resposta<BolaoSolicitacao> RecusarSolicitacao(RecusarBolaoSolicitacaoDTO recusarBolaoSolicitacaoDTO, int idUsuarioAcao)
        {
            if (!RulesBolaoSolicitacao.AptoPareRecusarSolicitacao(recusarBolaoSolicitacaoDTO, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesBolaoSolicitacao.ObterFalhas());
                return Resposta;
            }

            var bolaoSolicitacao = RepositorioBolaoSolicitacao.Obter(recusarBolaoSolicitacaoDTO.IdSolicitacao);
            bolaoSolicitacao.RecusarSolicitacao();

            RepositorioBolaoSolicitacao.Atualizar(bolaoSolicitacao);
            RepositorioBolaoSolicitacao.Salvar();

            Resposta.AdicionarConteudo(bolaoSolicitacao);
            return Resposta;
        }

        public Resposta<IEnumerable<BolaoSolicitacaoDTO>> ObterSolicitacoesPorBolao(int idBolao, int idUsuarioAcao)
        {
            if (!RulesBolaoSolicitacao.AptoParaObterSolicitacoes(idBolao, idUsuarioAcao))
            {
                RespostaListaDTO.AdicionarNotificacao(RulesBolaoSolicitacao.ObterFalhas());
                return RespostaListaDTO;
            }

            var solicitacoes = RepositorioBolaoSolicitacao.ObterSolicitacoesPorBolao(idBolao);

            RespostaListaDTO.AdicionarConteudo(solicitacoes);
            return RespostaListaDTO;
        }
    }
}
