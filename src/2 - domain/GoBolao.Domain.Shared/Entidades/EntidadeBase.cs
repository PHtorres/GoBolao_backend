using GoBolao.Domain.Shared.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Shared.Entidades
{
    public class EntidadeBase:Validacoes
    {
        public int Id { get; private set; }
    }
}
