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
    public class ClienteValidator : AbstractValidator<ClienteDto>
    {
        public ClienteValidator(IClienteRepository clienteRepository)
        {
            RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.");

            //RuleFor(c => c.Email)
            //    .NotEmpty().WithMessage("O e-mail é obrigatório.")
            //    .EmailAddress().WithMessage("Formato de e-mail inválido.")
            //    .MustAsync(async (email, cancellation) => (await clienteRepository.GetByEmailAsync(email) == null))
            //    .WithMessage("Já existe um cadastro com este e-mail.");

            RuleFor(c => c.Email)
                  .NotEmpty().WithMessage("O e-mail é obrigatório.")
                  .EmailAddress().WithMessage("Formato de e-mail inválido")
                  .MustAsync(async (email, cancellation) =>
                      (await clienteRepository.GetByEmailAsync(email)) == null)
                  .WithMessage("Já existe um cadastro com este e-mail.")
                  .When(c => !string.IsNullOrEmpty(c.Email));

            RuleFor(c => c.Cpf)
                .MustAsync(async (cpf, cancellation) =>
                    string.IsNullOrEmpty(cpf) || (await clienteRepository.GetByCpfCnpjAsync(cpf) == null))
                .WithMessage("Já existe um cadastro com este CPF.")
                .When(c => !string.IsNullOrEmpty(c.Cpf));

            RuleFor(c => c.Cnpj)
                .MustAsync(async (cnpj, cancellation) =>
                    string.IsNullOrEmpty(cnpj) || (await clienteRepository.GetByCpfCnpjAsync(cnpj) == null))
                .WithMessage("Já existe um cadastro com este CNPJ.")
                .When(c => !string.IsNullOrEmpty(c.Cnpj));

            RuleFor(c => c)
                .Must(c => string.IsNullOrEmpty(c.Cpf) || string.IsNullOrEmpty(c.Cnpj))
                .WithMessage("Um cliente não pode ter CPF e CNPJ ao mesmo tempo.");

            RuleFor(c => c.DataNascimento)
                .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
                .Must(data => CalcularIdade(data) >= 18)
                .WithMessage("A idade mínima para cadastro é 18 anos.")
                .When(c => !string.IsNullOrEmpty(c.Cpf));

            RuleFor(c => c.IsentoIE)
                .Must((c, isentoIE) =>
                    !string.IsNullOrEmpty(c.Cnpj) ? isentoIE : true)  
                .WithMessage("Pessoa Jurídica deve informar a IE ou ser isenta.")
                .When(c => !string.IsNullOrEmpty(c.Cnpj));
        }

        private int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }
    }
}
