using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Core.Entidades
{
    public class Time:EntidadeBase, IEntidade
    {
        public Time(string nome, string nomeImagemAvatar)
        {
            Nome = nome;
            NomeImagemAvatar = nomeImagemAvatar;

            Validar();
        }

        public string Nome { get; private set; }
        public string NomeImagemAvatar { get; private set; }

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


        public void Validar()
        {
            ValidarNome();
            ValidarNomeImagemAvatar();
        }

        private void ValidarNome()
        {
            NaoDeveSerVazio(Nome, "Nome precisa ser informado.");
            NaoDeveSerMaiorQue(50, Nome, "Nome deve ter, no máximo, 50 caracteres.");
        }

        private void ValidarNomeImagemAvatar()
        {
            NaoDeveSerMaiorQue(200, NomeImagemAvatar, "Nome da imagem deve ter, no máximo, 200 caracteres.");
            NaoDeveSerVazio(NomeImagemAvatar, "Nome da imagem deve ser informado.");
        }
    }
}
