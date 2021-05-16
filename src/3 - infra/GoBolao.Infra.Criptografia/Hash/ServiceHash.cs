using GoBolao.Domain.Shared.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GoBolao.Infra.Criptografia.Hash
{
    public class ServiceHash : IServiceCriptografia
    {
        private readonly HashAlgorithm algoritmo;

        public ServiceHash()
        {
            algoritmo = SHA512.Create();
        }

        public bool ConfereCriptografia(string texto, string criptografado)
        {
            var textoCriptografado = Criptografar(texto);

            return textoCriptografado == criptografado;
        }

        public string Criptografar(string texto)
        {
            var encodedValue = Encoding.UTF8.GetBytes(texto);
            var textoCriptografado = algoritmo.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var caracter in textoCriptografado)
            {
                sb.Append(caracter.ToString("X2")); //transformar em string / formatar cada caretctere em hexadecimal para fortalecer criptografia
            }

            return sb.ToString();
        }

        public void Dispose()
        {
            algoritmo.Dispose();
        }
    }
}
