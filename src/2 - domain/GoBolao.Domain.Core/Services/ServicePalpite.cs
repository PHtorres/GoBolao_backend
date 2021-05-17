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
    public class ServicePalpite : IServicePalpite
    {
        private readonly IRepositoryPalpite RepositorioPalpite;
        private readonly IRulesPalpite RulesPalpite;
        private Resposta<Palpite> Resposta;
        private Resposta<IEnumerable<PalpiteDTO>> RespostaListaDTO;

        public ServicePalpite(IRepositoryPalpite repositorioPalpite, IRulesPalpite rulesPalpite)
        {
            RepositorioPalpite = repositorioPalpite;
            Resposta = new Resposta<Palpite>();
            RespostaListaDTO = new Resposta<IEnumerable<PalpiteDTO>>();
            RulesPalpite = rulesPalpite;
        }

        public Resposta<Palpite> CriarPalpite(CriarPalpiteDTO criarPalpiteDTO, int idUsuarioAcao)
        {
            var palpite = new Palpite(criarPalpiteDTO.IdJogo, idUsuarioAcao, criarPalpiteDTO.PlacarMandantePalpite, criarPalpiteDTO.PlacarVisitantePalpite);
            if (palpite.Invalido)
            {
                Resposta.AdicionarNotificacao(palpite._Erros);
                return Resposta;
            }

            if (!RulesPalpite.AptoParaCriar(criarPalpiteDTO, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesPalpite.ObterFalhas());
                return Resposta;
            }

            RepositorioPalpite.Adicionar(palpite);
            RepositorioPalpite.Salvar();

            Resposta.AdicionarConteudo(palpite);
            return Resposta;
        }

        public void Dispose()
        {
            RepositorioPalpite.Dispose();
            RulesPalpite.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesPorUsuario(int idUsuario)
        {
            var palpites = RepositorioPalpite.ObterPalpitesPorUsuario(idUsuario);
            RespostaListaDTO.AdicionarConteudo(palpites);
            return RespostaListaDTO;
        }

        public Resposta<Palpite> RemoverPalpite(int idPalpite, int idUsuarioAcao)
        {
            if(!RulesPalpite.AptoParaRemover(idPalpite, idUsuarioAcao))
            {
                Resposta.AdicionarNotificacao(RulesPalpite.ObterFalhas());
                return Resposta;
            }

            var palpite = RepositorioPalpite.Obter(idPalpite);

            RepositorioPalpite.Remover(palpite);
            RepositorioPalpite.Salvar();

            Resposta.AdicionarConteudo(palpite);
            return Resposta;
        }
    }
}
