using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RommanelDev._Domain.Entities;
using RommanelDev.Application.Commands;
using RommanelDev.Application.DTO;
using RommanelDev.Application.Queries;

namespace RommanelDev.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<ClientDto> _validator;

        public ClientController(IMediator mediator, IValidator<ClientDto> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosClientes()
        {
            var query = new GetAllClientsQuery();
            var clientes = await _mediator.Send(query);

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterClientePorId(string id)
        {
            var query = new GetClientByIdQuery(id);
            var cliente = await _mediator.Send(query);

            if (cliente == null)
                return NotFound(); 

            return Ok(cliente); 
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] ClientDto clienteDto)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(clienteDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var command = new CreateClientCommand(
                    clienteDto.Name,
                    clienteDto.Cpf,
                    clienteDto.Cnpj,
                    clienteDto.BirthDate,
                    clienteDto.Phone,
                    clienteDto.Email,
                    clienteDto.Address,
                    clienteDto.FreeIE
                );

                var clienteId = await _mediator.Send(command);
               
                return Ok(new { id = clienteId });
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(string id, [FromBody] ClientDto clienteDto)
        {          
            var validationResult = await _validator.ValidateAsync(clienteDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var command = new UpdateClientCommand(
                id,
                clienteDto.Name,              
                clienteDto.BirthDate,
                clienteDto.Phone,               
                clienteDto.Address,
                clienteDto.FreeIE
            );

            await _mediator.Send(command);

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverCliente(string id)
        {
            var command = new RemoveClientCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
