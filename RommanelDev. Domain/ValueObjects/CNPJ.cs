using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.ValueObjects
{
    public record CNPJ
    {
        public string Value { get; }

        public CNPJ(string value)
        {
            if (!IsValidCNPJ(value))
                throw new ArgumentException("CNPJ inválido.");

            Value = value;
        }

        private bool IsValidCNPJ(string cnpj)
        {           
            return !string.IsNullOrEmpty(cnpj) && cnpj.Length == 14;
        }
    }
}
