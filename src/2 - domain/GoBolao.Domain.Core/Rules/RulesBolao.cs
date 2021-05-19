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
    public class RulesBolao : RulesBase, IRulesBolao
    {
        private readonly IRepositoryBolao RepositorioBolao;
        private readonly IRepositoryBolaoUsuario RepositorioBolaoUsuario;

        public RulesBolao(IRepositoryBolao repositorioBolao, IRepositoryBolaoUsuario repositorioBolaoUsuario)
        {
            RepositorioBolao = repositorioBolao;
            RepositorioBolaoUsuario = repositorioBolaoUsuario;
        }

        public bool AptoParaCriarBolao(CriarBolaoDTO criarBolaoDTO)
        {
            NomeDeveSerUnicoNaCriacao(criarBolaoDTO.Nome);
            return SemFalhas;
        }

        public bool AptoParaParticiparDeBolaoPublico(ParticiparDeBolaoPublicoDTO participarDeBolaoPublicoDTO, int idUsuarioAcao)
        {
            BolaoDeveExistir(participarDeBolaoPublicoDTO.IdBolao);
            BolaoDeveSerPublico(participarDeBolaoPublicoDTO.IdBolao);
            UsuarioNaoDeveEstarParticipandoDoBolao(participarDeBolaoPublicoDTO.IdBolao, idUsuarioAcao);
            return SemFalhas;
        }

        public bool AptoParaSairDoBolao(int idBolao, int idUsuarioAcao)
        {
            BolaoDeveExistir(idBolao);
            UsuarioDeveEstarParticipandoDoBolao(idBolao, idUsuarioAcao);
            UsuarioNaoDeveSerCriadorDoBolao(idBolao, idUsuarioAcao);
            return SemFalhas;
        }

        public void Dispose()
        {
            RepositorioBolao.Dispose();
            RepositorioBolaoUsuario.Dispose();
            GC.SuppressFinalize(this);
        }

        public IReadOnlyCollection<string> ObterFalhas()
        {
            return Falhas;
        }

        private void NomeDeveSerUnicoNaCriacao(string nome)
        {
            var boloesComMesmoNome = RepositorioBolao.ObterBoloesPeloNome(nome);

            if (boloesComMesmoNome.Any())
            {
                AdicionarFalha("Nome já em uso. Escolha outro, por favor!");
            }
        }

        private void BolaoDeveSerPublico(int idBolao)
        {
            var bolao = RepositorioBolao.Obter(idBolao);

            if(bolao.Privacidade != Privacidade.Publico)
            {
                AdicionarFalha("Bolão não é público.");
            }
        }

        private void UsuarioNaoDeveEstarParticipandoDoBolao(int idBolao, int idUsuario)
        {
            var usuarioBolao = RepositorioBolaoUsuario.ObterUsuariosDoBolao(idBolao).Where(ub => ub.IdUsuario == idUsuario);
            if (usuarioBolao.Any())
            {
                AdicionarFalha("Usuário já está participando do bolão.");
            }
        }

        private void UsuarioDeveEstarParticipandoDoBolao(int idBolao, int idUsuario)
        {
            var usuarioBolao = RepositorioBolaoUsuario.ObterUsuariosDoBolao(idBolao).Where(ub => ub.IdUsuario == idUsuario);
            if (!usuarioBolao.Any())
            {
                AdicionarFalha("Usuário não está participando do bolão.");
            }
        }

        private void BolaoDeveExistir(int idBolao)
        {
            var bolao = RepositorioBolao.Obter(idBolao);

            if(bolao == null)
            {
                AdicionarFalha("Bolão não existe.");
            }
        }

        private void UsuarioNaoDeveSerCriadorDoBolao(int idBolao, int idUsuario)
        {
            var bolao = RepositorioBolao.Obter(idBolao);
            
            if(bolao.IdCriador == idUsuario)
            {
                AdicionarFalha("Usuário é o criador do bolão.");
            }
        }
    }
}
