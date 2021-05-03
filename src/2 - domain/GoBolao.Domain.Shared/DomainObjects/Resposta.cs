using System.Collections.Generic;
using System.Linq;

namespace GoBolao.Domain.Shared.DomainObjects
{
    public class Resposta<ConteudoGenerico>
    {
        public Resposta()
        {
            Notificacoes = new List<string>();
            _Notificacoes = new List<string>();
        }
        private List<string> _Notificacoes { get; set; }
        public IReadOnlyCollection<string> Notificacoes { get { return _Notificacoes; } private set { } }
        public bool Sucesso { get { return !ExistemErros(); } private set { } }
        public ConteudoGenerico Conteudo { get; private set; }

        public void AdicionarNotificacao(string notificacao)
        {
            _Notificacoes.Add(notificacao);
        }

        public void AdicionarNotificacao(IReadOnlyCollection<string> notificacoes)
        {
            _Notificacoes.AddRange(notificacoes);
        }

        public void AdicionarNotificacao(List<string> notificacoes)
        {
            _Notificacoes.AddRange(notificacoes);
        }

        private bool ExistemErros()
        {
            return _Notificacoes.Any();
        }

        public void AdicionarConteudo(ConteudoGenerico conteudo)
        {
            Conteudo = conteudo;
        }
    }
}
