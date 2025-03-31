using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RommanelDev.Application.Commands;
using RommanelDev.Application.DTO;
using RommanelDev.Application.Queries;

namespace RommanelDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<ClienteDto> _validator;

        public ClienteController(IMediator mediator, IValidator<ClienteDto> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosClientes()
        {
            var query = new GetAllClientesQuery();
            var clientes = await _mediator.Send(query);

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(string id)
        {
            var query = new GetClienteByIdQuery(id);
            var cliente = await _mediator.Send(query);

            if (cliente == null)
                return NotFound(); 

            return Ok(cliente); 
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] ClienteDto clienteDto)
        {
            // Validar o clienteDto com o FluentValidation
            var validationResult = await _validator.ValidateAsync(clienteDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Enviar comando para criar o cliente
            var command = new CreateClienteCommand(
                clienteDto.Nome,
                clienteDto.Cpf,
                clienteDto.Cnpj,
                clienteDto.DataNascimento,
                clienteDto.Telefone,
                clienteDto.Email,
                clienteDto.IsentoIE
            );

            var clienteId = await _mediator.Send(command);

            return CreatedAtAction(nameof(ObterClientePorId), new { id = clienteId }, clienteId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(string id, [FromBody] ClienteDto clienteDto)
        {
            // Validar o clienteDto com o FluentValidation
            var validationResult = await _validator.ValidateAsync(clienteDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            // Enviar comando para atualizar o cliente
            var command = new UpdateClienteCommand(
                id,
                clienteDto.Nome,
                clienteDto.Cpf,
                clienteDto.Cnpj,
                clienteDto.DataNascimento,
                clienteDto.Telefone,
                clienteDto.Email,
                clienteDto.IsentoIE
            );

            await _mediator.Send(command);

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCliente(string id)
        {
            var command = new RemoveClienteCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
