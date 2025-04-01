using FluentValidation;
using RommanelDev._Domain.Contracts;
using RommanelDev.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Application.Validator
{
    public class ClientValidator : AbstractValidator<ClientDto>
    {
        public ClientValidator(IClientRepository clientRepository)
        {
            RuleFor(c => c.Name)
            .NotEmpty().WithMessage("O Name é obrigatório.");
           
            RuleFor(c => c.Email)
                  .NotEmpty().WithMessage("O e-mail é obrigatório.")
                  .EmailAddress().WithMessage("Formato de e-mail inválido")
                  .MustAsync(async (email, cancellation) =>
                      (await clientRepository.GetByEmailAsync(email)) == null)
                  .WithMessage("Já existe um cadastro com este e-mail.")
                  .When(c => !string.IsNullOrEmpty(c.Email));

            RuleFor(c => c.Cpf)
                .MustAsync(async (cpf, cancellation) =>
                    string.IsNullOrEmpty(cpf) || (await clientRepository.GetByCpfCnpjAsync(cpf) == null))
                .WithMessage("Já existe um cadastro com este CPF.")
                .When(c => !string.IsNullOrEmpty(c.Cpf));

            RuleFor(c => c.Cnpj)
                .MustAsync(async (cnpj, cancellation) =>
                    string.IsNullOrEmpty(cnpj) || (await clientRepository.GetByCpfCnpjAsync(cnpj) == null))
                .WithMessage("Já existe um cadastro com este CNPJ.")
                .When(c => !string.IsNullOrEmpty(c.Cnpj));

            RuleFor(c => c)
                .Must(c => string.IsNullOrEmpty(c.Cpf) || string.IsNullOrEmpty(c.Cnpj))
                .WithMessage("Um cliente não pode ter CPF e CNPJ ao mesmo tempo.");

            RuleFor(c => c.BirthDate)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .Must(data => CalcularIdade(data) >= 18)
                .WithMessage("A idade mínima para cadastro é 18 anos.")
                .When(c => !string.IsNullOrEmpty(c.Cpf));

            RuleFor(c => c.FreeIE)
                .Must((c, FreeIE) =>
                    !string.IsNullOrEmpty(c.Cnpj) ? FreeIE : true)  
                .WithMessage("Pessoa Jurídica deve informar a IE ou ser isenta.")
                .When(c => !string.IsNullOrEmpty(c.Cnpj));

            RuleFor(c => c.Address)
                .NotNull().WithMessage("O endereço é obrigatório.")
                .DependentRules(() =>
                {
                    RuleFor(c => c.Address).SetValidator(new AddressValidator());
                });
        }

        private int CalcularIdade(DateTime BirthDate)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - BirthDate.Year;
            if (BirthDate.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }
    }
}
