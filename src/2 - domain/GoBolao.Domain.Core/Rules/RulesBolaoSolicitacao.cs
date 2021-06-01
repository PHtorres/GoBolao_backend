using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Core.Interfaces.Rules;
using GoBolao.Domain.Core.ValueObjects;
using GoBolao.Domain.Shared.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoBolao.Domain.Core.Rules
{
    public class RulesBolaoSolicitacao : RulesBase, IRulesBolaoSolicitacao
    {
        private readonly IRepositoryBolaoSolicitacao RepositorioBolaoSolicitacao;
        private readonly IRepositoryBolaoUsuario RepositorioBolaoUsuario;
        private readonly IRepositoryBolao RepositorioBolao;

        public RulesBolaoSolicitacao(IRepositoryBolaoSolicitacao repositorioBolaoSolicitacao, IRepositoryBolaoUsuario repositorioBolaoUsuario, IRepositoryBolao repositorioBolao)
        {
            RepositorioBolaoSolicitacao = repositorioBolaoSolicitacao;
            RepositorioBolaoUsuario = repositorioBolaoUsuario;
            RepositorioBolao = repositorioBolao;
        }

        public bool AptoParaAceitarSolicitacao(AceitarBolaoSolicitacaoDTO aceitarBolaoSolicitacaoDTO, int idUsuarioAcao)
        {
            SolicitacaoDeveExistir(aceitarBolaoSolicitacaoDTO.IdSolicitacao);
            UsuarioAcaoDeveSerCriadorBolao(aceitarBolaoSolicitacaoDTO.IdSolicitacao, idUsuarioAcao);
            return SemFalhas;
        }

        public bool AptoParaCriarSolicitacao(CriarBolaoSolicitacaoDTO criarBolaoSolicitacaoDTO, int idUsuarioAcao)
        {
            UsuarioSolicitanteNaoDeveEstarParticipandoDoBolao(criarBolaoSolicitacaoDTO.IdBolao, idUsuarioAcao);
            UsuarioSolicitanteNaoDeveTerSolicitacaoAbertaComOBolao(criarBolaoSolicitacaoDTO.IdBolao, idUsuarioAcao);
            return SemFalhas;
        }

        public bool AptoParaDesfazerSolicitacao(int idSolicitacao, int idUsuarioAcao)
        {
            SolicitacaoDeveExistir(idSolicitacao);
            UsuarioAcaoDeveSerUsuarioSolicitante(idSolicitacao, idUsuarioAcao);
            return SemFalhas;
        }

        public bool AptoParaObterSolicitacoes(int idBolao, int idUsuarioAcao)
        {
            UsuarioAcaoDeveSerCriadorBolaoParaObter(idBolao, idUsuarioAcao);
            return SemFalhas;
        }

        public bool AptoPareRecusarSolicitacao(RecusarBolaoSolicitacaoDTO recusarBolaoSolicitacaoDTO, int idUsuarioAcao)
        {
            SolicitacaoDeveExistir(recusarBolaoSolicitacaoDTO.IdSolicitacao);
            UsuarioAcaoDeveSerCriadorBolao(recusarBolaoSolicitacaoDTO.IdSolicitacao, idUsuarioAcao);
            return SemFalhas;
        }

        public void Dispose()
        {
            RepositorioBolaoSolicitacao.Dispose();
            RepositorioBolaoUsuario.Dispose();
            RepositorioBolao.Dispose();
            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<string> ObterFalhas()
        {
            return Falhas;
        }

        private void SolicitacaoDeveExistir(int idSolicitacao)
        {
            var solicitacao = RepositorioBolaoSolicitacao.Obter(idSolicitacao);
            if(solicitacao == null)
            {
                AdicionarFalha("Solicitação não existe.");
            }
        }

        private void UsuarioAcaoDeveSerCriadorBolao(int idSolicitacao, int idUsuarioAcao)
        {
            var bolaoSolicitacao = RepositorioBolaoSolicitacao.Obter(idSolicitacao);
            var bolao = RepositorioBolao.Obter(bolaoSolicitacao.IdBolao);
            if(bolao != null)
            {
                if(bolao.IdCriador != idUsuarioAcao)
                {
                    AdicionarFalha("Apenas o criador do bolão podo executar esta ação.");
                }
            }
        }

        private void UsuarioAcaoDeveSerCriadorBolaoParaObter(int idBolao, int idUsuarioAcao)
        {
            var bolao = RepositorioBolao.Obter(idBolao);
            if (bolao != null)
            {
                if (bolao.IdCriador != idUsuarioAcao)
                {
                    AdicionarFalha("Apenas o criador do bolão podo executar esta ação.");
                }
            }
        }

        private void UsuarioSolicitanteNaoDeveEstarParticipandoDoBolao(int idBolao, int idUsuarioSolicitante)
        {
            var bolaoUsuarios = RepositorioBolaoUsuario.ObterUsuariosDoBolao(idBolao);
            if (bolaoUsuarios.Any())
            {
                if(bolaoUsuarios.Where(bu => bu.IdUsuario == idUsuarioSolicitante).Any())
                {
                    AdicionarFalha("Usuário solicitante já está participando do bolão.");
                }
            }
        }

        private void UsuarioSolicitanteNaoDeveTerSolicitacaoAbertaComOBolao(int idBolao, int idUsuarioSolicitante)
        {
            var solicitacoesUsuarioAbertas = RepositorioBolaoSolicitacao.Listar()
                .Where(bs => bs.IdBolao == idBolao && bs.IdUsuarioSolicitante == idUsuarioSolicitante && bs.Status == StatusBolaoSolicitacao.Aberta);
            if (solicitacoesUsuarioAbertas.Any())
            {
                AdicionarFalha("Já existe solicitação aberta para este bolão. Aguarde resposta do criador do bolão");
            }
        }

        private void UsuarioAcaoDeveSerUsuarioSolicitante(int idSolicitacao, int idUsuarioAcao)
        {
            var bolaoSolicitacao = RepositorioBolaoSolicitacao.Obter(idSolicitacao);
            if(bolaoSolicitacao != null)
            {
                if(bolaoSolicitacao.IdUsuarioSolicitante != idUsuarioAcao)
                {
                    AdicionarFalha("Apenas o usuário solicitante pode desfazer a solicitação.");
                }
            }
        }
    }
}
