using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;

namespace GoBolao.Domain.Core.Entidades
{
    public sealed class Campeonato : EntidadeBase, IEntidade
    {
        public Campeonato(string nome)
        {
            Nome = nome;
            NomeImagemAvatar = "";
        }

        public string Nome { get; private set; }
        public string NomeImagemAvatar { get; private set; }

        public void Validar()
        {
            ValidarNome();
            ValidarNomeImagemAvatar();
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
            ValidarNome();
        }

        public void AlterarNomeImagemAvatar(string nomeImagemAvatar)
        {
            NomeImagemAvatar = nomeImagemAvatar;
            ValidarNomeImagemAvatar();
        }

        private void ValidarNome()
        {
            NaoDeveSerVazio(Nome, "Nome precisa ser informado.");
            NaoDeveSerMaiorQue(50, Nome, "Nome deve ter, no máximo, 50 caracteres.");
            NaoDeveSerMenorQue(4, Nome, "Nome deve ter, pelo menos, 4 caracteres.");
        }

        private void ValidarNomeImagemAvatar()
        {
            NaoDeveSerMaiorQue(200, NomeImagemAvatar, "Url do avatar deve ter, no máximo, 200 caracteres.");
        }
    }
}
