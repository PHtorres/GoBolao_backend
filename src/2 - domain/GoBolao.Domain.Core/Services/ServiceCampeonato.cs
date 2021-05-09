using GoBolao.Domain.Core.DTO;
using GoBolao.Domain.Core.Entidades;
using GoBolao.Domain.Core.Interfaces.Repository;
using GoBolao.Domain.Core.Interfaces.Service;
using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;

namespace GoBolao.Domain.Core.Services
{
    public class ServiceCampeonato : IServiceCampeonato
    {
        private readonly IRepositoryCampeonato RepositorioCampeonato;
        private Resposta<Campeonato> Resposta;

        public ServiceCampeonato(IRepositoryCampeonato repositorioCampeonato)
        {
            RepositorioCampeonato = repositorioCampeonato;
            Resposta = new Resposta<Campeonato>();
        }

        public Resposta<Campeonato> CriarCampeonato(CriarCampeonatoDTO criarcampeonatodto)
        {
            var campeonato = new Campeonato(criarcampeonatodto.Nome);
            if (campeonato.Invalido)
            {
                Resposta.AdicionarNotificacao(campeonato._Erros);
                return Resposta;
            }

            RepositorioCampeonato.Adicionar(campeonato);
            RepositorioCampeonato.Salvar();

            Resposta.AdicionarConteudo(campeonato);
            return Resposta;
        }

        public Resposta<Campeonato> AlterarNomeImagemAvatar(AlterarNomeImagemAvatarCampeonatoDTO alterarUrlAvatarCampeonatoDTO)
        {
            var campeonato = RepositorioCampeonato.Obter(alterarUrlAvatarCampeonatoDTO.IdCampeonato);
            campeonato.AlterarNomeImagemAvatar(alterarUrlAvatarCampeonatoDTO.NomeImagemAvatar);

            if (campeonato.Invalido)
            {
                Resposta.AdicionarNotificacao(campeonato._Erros);
                return Resposta;
            }

            RepositorioCampeonato.Atualizar(campeonato);
            RepositorioCampeonato.Salvar();

            Resposta.AdicionarConteudo(campeonato);
            return Resposta;
        }

        public Resposta<IEnumerable<Campeonato>> ObterCampeonatos()
        {
            var respostaLista = new Resposta<IEnumerable<Campeonato>>();
            respostaLista.AdicionarConteudo(RepositorioCampeonato.Listar());
            return respostaLista;
        }

        public void Dispose()
        {
            RepositorioCampeonato.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
