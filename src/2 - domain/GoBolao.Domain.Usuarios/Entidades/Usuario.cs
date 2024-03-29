﻿using GoBolao.Domain.Shared.Entidades;
using GoBolao.Domain.Shared.Interfaces.Entidade;

namespace GoBolao.Domain.Usuarios.Entidades
{
    public sealed class Usuario:EntidadeBase, IEntidade
    {
        public Usuario(string apelido, string email, string senha)
        {
            Apelido = apelido;
            Email = email;
            Senha = senha;
            NomeImagemAvatar = "";
            Validar();
        }

        public string Apelido { get; private set; }
		public string Email { get; private set; }
		public string Senha { get; private set; }
		public string NomeImagemAvatar { get; private set; }

        public void Validar()
        {
            ValidarApelido();
            ValidarEmail();
            ValidarSenha();
            ValidarNomeImagemAvatar();
        }

        public void AlterarApelido(string apelido)
        {
            Apelido = apelido;
            ValidarApelido();
        }

        public void AlterarEmail(string email)
        {
            Email = email;
            ValidarEmail();
        }

        public void AlterarSenha(string senha)
        {
            Senha = senha;
            ValidarSenha();
        }

        public void AlterarNomeImagemAvatar(string nomeImagemAvatar)
        {
            NomeImagemAvatar = nomeImagemAvatar;
            ValidarNomeImagemAvatar();
        }

        private void ValidarApelido()
        {
            NaoDeveSerMaiorQue(30, Apelido, "Apelido nao deve conter mais de 30 caracteres");
            NaoDeveSerMenorQue(4, Apelido, "Apelido deve conter pelo menos 4 caracteres");
        }

        private void ValidarEmail()
        {
            EmailDeveSerValido(Email, "E-Mail inválido.");
            NaoDeveSerMaiorQue(50, Email, "E-mail não deve conter mais de 50 caracteres.");
        }

        private void ValidarSenha()
        {
            NaoDeveSerVazio(Senha, "Informe uma senha.");
            NaoDeveSerMenorQue(5, Senha, "A senha deve conter, pelo menos, 5 caracteres");
        }

        private void ValidarNomeImagemAvatar()
        {
            NaoDeveSerMaiorQue(200, NomeImagemAvatar, "Nome da imagem do avatar muito grande.");
        }
    }
}
