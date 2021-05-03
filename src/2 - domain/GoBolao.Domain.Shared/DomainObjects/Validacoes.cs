using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace GoBolao.Domain.Shared.DomainObjects
{
    public abstract class Validacoes
    {
        public Validacoes()
        {
            Erros = new List<string>();
        }

        [NotMapped]
        private List<string> Erros { get; set; }
        [NotMapped]
        public IReadOnlyList<string> _Erros { get { return Erros; } private set { } }
        [NotMapped]
        public bool Invalido { get { return ExistemErros(); } private set { } }
        [NotMapped]
        public bool Valido { get { return NaoExistemErros(); } private set { } }

        private void AdicionarErro(string mensagem)
        {
            Erros.Add(mensagem);
        }

        private bool ExistemErros()
        {
            return Erros.Any();
        }

        private bool NaoExistemErros()
        {
            return !Erros.Any();
        }

        protected void NaoDeveSerVazio(string obj, string mensagemErro)
        {
            if (obj.Vazio())
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void DeveSerMenorQue(int minimo, string obj, string mensagemErro)
        {
            if (!obj.MenorQue(minimo))
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void DeveSerMaiorQue(int maximo, string obj, string mensagemErro)
        {
            if (!obj.MaiorQue(maximo))
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void NaoDeveSerMenorQue(int minimo, string obj, string mensagemErro)
        {
            if (obj.MenorQue(minimo))
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void NaoDeveSerMaiorQue(int maximo, string obj, string mensagemErro)
        {
            if (obj.MaiorQue(maximo))
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void DeveSerMenorQue(int minimo, int obj, string mensagemErro)
        {
            if (!obj.MenorQue(minimo))
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void DeveSerMaiorQue(int maximo, int obj, string mensagemErro)
        {
            if (!obj.MaiorQue(maximo))
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void NaoDeveSerMenorQue(int minimo, int obj, string mensagemErro)
        {
            if (obj.MenorQue(minimo))
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void NaoDeveSerMaiorQue(int maximo, int obj, string mensagemErro)
        {
            if (obj.MaiorQue(maximo))
            {
                AdicionarErro(mensagemErro);
            }
        }

        protected void EmailDeveSerValido(string email, string mensagemErro)
        {
            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch
            {
                AdicionarErro(mensagemErro);
            }
        }
    }
}
