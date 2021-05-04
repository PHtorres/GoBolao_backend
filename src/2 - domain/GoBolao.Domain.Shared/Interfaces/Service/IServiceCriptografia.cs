using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Shared.Interfaces.Service
{
    public interface IServiceCriptografia:IDisposable
    {
        string Criptografar(string texto);
        bool ConfereCriptografia(string texto, string textoCriptografado);
    }
}
