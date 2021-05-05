using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;

namespace GoBolao.Domain.Core.Entidades
{
    public sealed class Campeonato : EntidadeBase, IEntidade
    {
        public Campeonato(string nome)
        {
            Nome = nome;
            UrlAvatar = "";
        }

        public string Nome { get; private set; }
        public string UrlAvatar { get; private set; }

        public void Validar()
        {
            ValidarNome();
            ValidarUrlAvatar();
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
            ValidarNome();
        }

        public void AlterarUrlAvatar(string urlAvatar)
        {
            UrlAvatar = urlAvatar;
            ValidarUrlAvatar();
        }

        private void ValidarNome()
        {
            NaoDeveSerVazio(Nome, "Nome precisa ser informado.");
            NaoDeveSerMaiorQue(50, Nome, "Nome deve ter, no máximo, 50 caracteres.");
            NaoDeveSerMenorQue(4, Nome, "Nome deve ter, pelo menos, 4 caracteres.");
        }

        private void ValidarUrlAvatar()
        {
            NaoDeveSerMaiorQue(100, UrlAvatar, "Url do avatar deve ter, no máximo, 100 caracteres.");
        }
    }
}
