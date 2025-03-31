using FluentValidation;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Validator
{
    public class EnderecoValidator : AbstractValidator<EnderecoDto>
    {
        public EnderecoValidator()
        {
            RuleFor(e => e.CEP)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Matches(@"^\d{8}$").WithMessage("O CEP deve conter exatamente 8 dígitos numéricos.");

            RuleFor(e => e.Logradouro)
                .NotEmpty().WithMessage("O logradouro é obrigatório.");

            RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("O número é obrigatório.");

            RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("O bairro é obrigatório.");

            RuleFor(e => e.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.");

            RuleFor(e => e.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .Length(2).WithMessage("O estado deve ter exatamente 2 caracteres.");
        }
    }
}
