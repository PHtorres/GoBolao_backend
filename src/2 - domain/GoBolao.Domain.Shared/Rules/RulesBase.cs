using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Shared.Rules
{
    public abstract class RulesBase
    {
        public RulesBase()
        {
            _Falhas = new List<string>();
        }
        private List<string> _Falhas { get; set; }
        protected IReadOnlyCollection<string> Falhas { get { return _Falhas; } private set { } }
        protected void AdicionarFalha(string falha)
        {
            _Falhas.Add(falha);
        }
    }
}
