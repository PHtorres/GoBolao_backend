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
    public class ServicePalpite : IServicePalpite
    {
        private readonly IRepositoryPalpite RepositorioPalpite;
        private readonly IRepositoryBolao RepositorioBolao;
        private readonly IRulesPalpite RulesPalpite;
        private Resposta<Palpite> Resposta;
        private Resposta<IEnumerable<PalpiteDTO>> RespostaListaDTO;

        public ServicePalpite(IRepositoryPalpite repositorioPalpite, IRulesPalpite rulesPalpite, IRepositoryBolao repositorioBolao)
        {
            RepositorioPalpite = repositorioPalpite;
            Resposta = new Resposta<Palpite>();
            RespostaListaDTO = new Resposta<IEnumerable<PalpiteDTO>>();
            RulesPalpite = rulesPalpite;
            RepositorioBolao = repositorioBolao;
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
            RepositorioBolao.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesAbertosPorUsuario(int idUsuarioAcao)
        {
            var palpitesAbertos = RepositorioPalpite.ObterPalpitesPorUsuario(idUsuarioAcao).Where(p => p.Finalizado == false);
            RespostaListaDTO.AdicionarConteudo(palpitesAbertos);
            return RespostaListaDTO;
        }

        public Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesFinalizadosPorUsuario(int idUsuarioAcao)
        {
            var palpitesFinalizados = RepositorioPalpite.ObterPalpitesPorUsuario(idUsuarioAcao).Where(p => p.Finalizado == true);
            RespostaListaDTO.AdicionarConteudo(palpitesFinalizados);
            return RespostaListaDTO;
        }

        public Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesFinalizadosPorUsuarioDeUmBolao(int idUsuario, int idBolao)
        {
            var palpitesBolao = RepositorioPalpite.ObterPalpitesPorBolao(idBolao);
            var palpitesUsuario = palpitesBolao.Where(p => p.IdUsuarioPalpite == idUsuario && p.Finalizado);
            RespostaListaDTO.AdicionarConteudo(palpitesUsuario);
            return RespostaListaDTO;
        }

        public Resposta<IEnumerable<PalpiteDTO>> ObterPalpitesPorJogoFinalizadoOuIniciadoDosAdiversarios(int idJogo, int idUsuarioAcao)
        {
            var palpitesDoJogo = RepositorioPalpite.ObterPalpitesPorJogoDTO(idJogo).Where(p => p.Finalizado == true || p.DataHoraJogo < DateTime.Now);
            var adversariosBoloes = RepositorioBolao.ObterAdiversariosBoloes(idUsuarioAcao);
            var palpitesDoJogoDosAdiversarios = palpitesDoJogo.Where(p => adversariosBoloes.Where(a => a.IdMembro == p.IdUsuarioPalpite).Any());
            RespostaListaDTO.AdicionarConteudo(palpitesDoJogoDosAdiversarios);
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
