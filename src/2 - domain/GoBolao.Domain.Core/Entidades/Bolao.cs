using GoBolao.Domain.Core.ValueObjects;
using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;

namespace GoBolao.Domain.Core.Entidades
{
    public class Bolao : EntidadeBase, IEntidade
    {
        public Bolao(string nome, int idCriador, int idCampeonato, Privacidade privacidade)
        {
            Nome = nome;
            IdCriador = idCriador;
            IdCampeonato = idCampeonato;
            Privacidade = privacidade;
            NomeImagemAvatar = "";
            Validar();
        }

        public string Nome { get; private set; }
        public int IdCriador { get; private set; }
        public int IdCampeonato { get; private set; }
        public Privacidade Privacidade { get; private set; }
        public string NomeImagemAvatar { get; private set; }

        public void AlterarNome(string nome)
        {
            Nome = nome;
            ValidarNome();
        }

        public void AlterarPrivacidade(Privacidade privacidade)
        {
            Privacidade = privacidade;
        }

        public void AlterarNomeImagemAvatar(string nomeImagemAvatar)
        {
            NomeImagemAvatar = nomeImagemAvatar;
            ValidarNomeImagemAvatar();
        }

        public void Validar()
        {
            ValidarNome();
            ValidarIdCriador();
            ValidarIdCampeonato();
            ValidarNomeImagemAvatar();
        }

        private void ValidarNome()
        {
            NaoDeveSerMenorQue(4, Nome, "Nome precisa conter, pelo menos, 4 caracteres.");
            NaoDeveSerMaiorQue(20, Nome, "Nome precisa conter, no máximo, 20 caracteres.");
        }

        private void ValidarIdCriador()
        {
            NaoDeveSerZeroOuMenos(IdCriador, "Id do criador inválido.");
        }

        private void ValidarIdCampeonato()
        {
            NaoDeveSerZeroOuMenos(IdCampeonato, "Id do campeonato inválido.");
        }

        private void ValidarNomeImagemAvatar()
        {
            NaoDeveSerMaiorQue(200, NomeImagemAvatar, "Nome da imagem do avatar deve conter, no máximo, 200 caracteres.");
        }
    }
}
