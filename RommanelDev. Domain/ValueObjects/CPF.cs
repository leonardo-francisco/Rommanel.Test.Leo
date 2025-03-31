using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.ValueObjects
{
    public record CPF
    {
        public string Value { get; }

        public CPF(string value)
        {
            if (!IsValidCPF(value))
                throw new ArgumentException("CPF inválido.");

            Value = value;
        }

        private bool IsValidCPF(string cpf)
        {          
            return !string.IsNullOrEmpty(cpf) && cpf.Length == 11;
        }
    }
}
