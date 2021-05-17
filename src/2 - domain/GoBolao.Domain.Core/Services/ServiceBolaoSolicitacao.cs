using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
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
        private Resposta<BolaoSolicitacao> Resposta;

        public ServiceBolaoSolicitacao(IRepositoryBolaoSolicitacao repositorioBolaoSolicitacao, IRepositoryBolaoUsuario repositorioBolaoUsuario)
        {
            RepositorioBolaoSolicitacao = repositorioBolaoSolicitacao;
            RepositorioBolaoUsuario = repositorioBolaoUsuario;
            Resposta = new Resposta<BolaoSolicitacao>();
        }

        public void Dispose()
        {
            RepositorioBolaoSolicitacao.Dispose();
            RepositorioBolaoUsuario.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<BolaoSolicitacao> AceitarSolicitacao(AceitarBolaoSolicitacaoDTO aceitarBolaoSolicitacaoDTO, int idUsuarioAcao)
        {
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
            throw new NotImplementedException();
        }

        public Resposta<BolaoSolicitacao> DesfazerSolicitacao(int idSolicitacao, int idUsuarioAcao)
        {
            throw new NotImplementedException();
        }

        public Resposta<BolaoSolicitacao> RecusarSolicitacao(RecusarBolaoSolicitacaoDTO recusarBolaoSolicitacaoDTO, int idUsuarioAcao)
        {
            throw new NotImplementedException();
        }
    }
}
