using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Shared.Interfaces.Rules
{
    public interface IRules:IDisposable
    {
        IReadOnlyCollection<string> ObterFalhas();
    }
}
