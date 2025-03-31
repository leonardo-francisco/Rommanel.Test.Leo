using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev._Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (!IsValidEmail(value))
                throw new ArgumentException("E-mail inválido.");

            Value = value;
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@"); 
        }
    }
}
