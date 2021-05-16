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
    public class ServiceTime : IServiceTime
    {
        private readonly IRepositoryTime RepositorioTime;
        private Resposta<Time> Resposta;
        private readonly IRulesTime RulesTime;

        public ServiceTime(IRepositoryTime repositorioTime, IRulesTime rulesTime)
        {
            RepositorioTime = repositorioTime;
            Resposta = new Resposta<Time>();
            RulesTime = rulesTime;
        }

        public Resposta<Time> AlterarTime(AlterarTimeDTO alterarTimeDTO)
        {
            var time = RepositorioTime.Obter(alterarTimeDTO.Id);
            time.AlterarNome(alterarTimeDTO.Nome);
            time.AlterarNomeImagemAvatar(alterarTimeDTO.NomeImagemAvatar);

            if (time.Invalido)
            {
                Resposta.AdicionarNotificacao(time._Erros);
                return Resposta;
            }

            if (!RulesTime.AptoParaAlterar(alterarTimeDTO))
            {
                Resposta.AdicionarNotificacao(RulesTime.ObterFalhas());
                return Resposta;
            }

            RepositorioTime.Atualizar(time);
            RepositorioTime.Salvar();

            Resposta.AdicionarConteudo(time);
            return Resposta;
        }

        public Resposta<Time> CriarTime(CriarTimeDTO criarTimeDTO)
        {
            var time = new Time(criarTimeDTO.Nome, criarTimeDTO.NomeImagemAvatar);
            if (time.Invalido)
            {
                Resposta.AdicionarNotificacao(time._Erros);
                return Resposta;
            }

            if (!RulesTime.AptoParaCriar(criarTimeDTO))
            {
                Resposta.AdicionarNotificacao(RulesTime.ObterFalhas());
                return Resposta;
            }

            RepositorioTime.Adicionar(time);
            RepositorioTime.Salvar();

            Resposta.AdicionarConteudo(time);
            return Resposta;
        }

        public void Dispose()
        {
            RepositorioTime.Dispose();
            RulesTime.Dispose();
            GC.SuppressFinalize(this);
        }

        public Resposta<IEnumerable<Time>> ObterTimes()
        {
            var respostaLista = new Resposta<IEnumerable<Time>>();
            respostaLista.AdicionarConteudo(RepositorioTime.Listar());
            return respostaLista;
        }
    }
}
