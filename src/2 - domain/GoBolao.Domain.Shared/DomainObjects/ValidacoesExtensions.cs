using System;
using System.Collections.Generic;
using System.Text;

namespace GoBolao.Domain.Shared.DomainObjects
{
    public static class ValidacoesExtentions
    {
        public static bool Vazio(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

        public static bool MaiorQue(this string obj, int maximo)
        {
            return obj.Length > maximo;
        }

        public static bool MaiorQue(this int obj, int maximo)
        {
            return obj > maximo;
        }

        public static bool MenorQue(this string obj, int minimo)
        {
            return obj.Length < minimo;
        }

        public static bool MenorQue(this int obj, int minimo)
        {
            return obj < minimo;
        }
    }
}
