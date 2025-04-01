using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RommanelDev._Domain.Entities;
using RommanelDev.API.Controllers;
using RommanelDev.Application.Commands;
using RommanelDev.Application.DTO;
using RommanelDev.Application.Queries;
using RommanelDev.Test.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RommanelDev.Test
{
    public class ClientCRUDTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IValidator<ClientDto>> _validatorMock;
        private readonly ClientController _controller;

        public ClientCRUDTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _validatorMock = new Mock<IValidator<ClientDto>>();
            _controller = new ClientController(_mediatorMock.Object, _validatorMock.Object);
        }

        #region Basic Validations
        [Fact]
        public async Task ObterTodosClientes_DeveRetornarOkComClientes()
        {
            // Arrange           
            var clientes = GetAllClientDtoTest.GetDefault();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllClientsQuery>(), default)).ReturnsAsync(clientes);

            // Act
            var result = await _controller.ObterTodosClientes();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(clientes);
        }

        [Fact]
        public async Task ObterClientePorId_DeveRetornarNotFoundSeClienteNaoExistir()
        {
            // Arrange
            //var cliente = GetClientDtoPFTest.GetDefault();
            var clienteId = "1"; // Identificador do cliente
            _mediatorMock.Setup(m => m.Send(It.Is<GetClientByIdQuery>(q => q.Id == clienteId), default)).ReturnsAsync((ClientDto)null);

            // Act
            var result = await _controller.ObterClientePorId("1");

            // Assert
            var notFoundResult = result as NotFoundResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task ObterClientePorId_DeveRetornarOkComCliente()
        {
            // Arrange
            var cliente = GetClientDtoPFTest.GetDefault();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetClientByIdQuery>(), default)).ReturnsAsync(cliente);

            // Act
            var result = await _controller.ObterClientePorId("1");

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(cliente);
        }

        [Fact]
        public async Task CriarCliente_DeveRetornarBadRequestSeValidationFalhar()
        {
            // Arrange
            var clienteDto = new ClientDto();
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Name", "Name is required") });
            _validatorMock.Setup(v => v.ValidateAsync(clienteDto, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _controller.CriarCliente(clienteDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CriarCliente_DeveRetornarOkComId()
        {
            // Arrange
            var clienteDto = new ClientDto { Name = "Client 1" };
            var validationResult = new ValidationResult();
            _validatorMock.Setup(v => v.ValidateAsync(clienteDto, default)).ReturnsAsync(validationResult);
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateClientCommand>(), default)).ReturnsAsync("1");

            // Act
            var result = await _controller.CriarCliente(clienteDto);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(new { id = "1" });
        }

        [Fact]
        public async Task AtualizarCliente_DeveRetornarBadRequestSeValidationFalhar()
        {
            // Arrange
            var clienteDto = new ClientDto();
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Name", "Name is required") });
            _validatorMock.Setup(v => v.ValidateAsync(clienteDto, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _controller.AtualizarCliente("1", clienteDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);
        }
        #endregion

        #region Custom Validations
        [Fact]
        public async Task CriarClientePF_DeveRetornarBadRequestSeMenorDe18Anos()
        {
            // Arrange
            var clientDto = GetClientDtoPFTest.GetDefault();
            clientDto.BirthDate = DateTime.Parse("2010-01-01");

            // Simulando que a validação falhou
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("BirthDate", "A idade do cliente deve ser maior ou igual a 18 anos.")
            });

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ClientDto>(), default))
                          .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.CriarCliente(clientDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);

            var errors = badRequestResult.Value as IEnumerable<ValidationFailure>;
            errors.Should().ContainSingle(e => e.PropertyName == "BirthDate" && e.ErrorMessage == "A idade do cliente deve ser maior ou igual a 18 anos.");
        }

        [Fact]
        public async Task CriarClientePJ_DeveRetornarBadRequestSeNaoTiverIE()
        {
            // Arrange
            var clientDto = GetClientDtoPJTest.GetDefault();
            clientDto.FreeIE = false;

            // Simulando que a validação falhou
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("FreeIE", "Pessoa Jurídica deve informar a IE ou ser isenta.")
            });

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ClientDto>(), default))
                          .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.CriarCliente(clientDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);

            var errors = badRequestResult.Value as IEnumerable<ValidationFailure>;
            errors.Should().ContainSingle(e => e.PropertyName == "FreeIE" && e.ErrorMessage == "Pessoa Jurídica deve informar a IE ou ser isenta.");
        }

        [Fact]
        public async Task CriarClientePJ_DeveRetornarBadRequestSeRepetirDocumento()
        {
            // Arrange
            var clientDto = GetClientDtoPJTest.GetDefault();
            

            // Simulando que a validação falhou
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Cnpj", "Já existe um cadastro com este CNPJ.")
            });

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ClientDto>(), default))
                          .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.CriarCliente(clientDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);

            var errors = badRequestResult.Value as IEnumerable<ValidationFailure>;
            errors.Should().ContainSingle(e => e.PropertyName == "Cnpj" && e.ErrorMessage == "Já existe um cadastro com este CNPJ.");
        }

        [Fact]
        public async Task CriarClientePF_DeveRetornarBadRequestSeRepetirDocumento()
        {
            // Arrange
            var clientDto = GetClientDtoPFTest.GetDefault();


            // Simulando que a validação falhou
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Cpf", "Já existe um cadastro com este CPF.")
            });

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ClientDto>(), default))
                          .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.CriarCliente(clientDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);

            var errors = badRequestResult.Value as IEnumerable<ValidationFailure>;
            errors.Should().ContainSingle(e => e.PropertyName == "Cpf" && e.ErrorMessage == "Já existe um cadastro com este CPF.");
        }

        [Fact]
        public async Task CriarCliente_DeveRetornarBadRequestSeRepetirEmail()
        {
            // Arrange
            var clientDto = GetClientDtoPFTest.GetDefault();
            clientDto.Cpf = "82484588062";


            // Simulando que a validação falhou
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Email", "Já existe um cadastro com este e-mail.")
            });

            _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ClientDto>(), default))
                          .ReturnsAsync(validationResult);

            // Act
            var result = await _controller.CriarCliente(clientDto);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be(400);

            var errors = badRequestResult.Value as IEnumerable<ValidationFailure>;
            errors.Should().ContainSingle(e => e.PropertyName == "Email" && e.ErrorMessage == "Já existe um cadastro com este e-mail.");
        }
        #endregion


    }
}
