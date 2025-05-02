using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.Application.Customers.Command.CreateCustomer;
using Rental.Application.Customers.Command.DeleteCustomer;
using Rental.Application.Customers.Command.TempDeleteCustomer;
using Rental.Application.Customers.Command.UpdateCustomer;
using Rental.Application.Customers.DTOs;
using Rental.Application.Customers.Queries.GetAllCustomers;
using Rental.Application.Customers.Queries.GetCustomersById;

namespace Rental.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomersDtos>>> GetAllCustomers()
        {
            var customers = await mediator.Send(new GetAllCustomersQuery());
            return Ok(customers);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomersDtos>> GetCustomerById([FromRoute] Guid id)
        {
            var customer = await mediator.Send(new GetCustomersByIdQuery(id));
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromForm] CreateCustomerCommand command)
        {
            var customerId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, null);
        }

        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, UpdateCustomerCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> TempDeleteCustomer([FromRoute] Guid id, TempDeleteCustomerCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCustomer([FromRoute] Guid id, DeleteCustomerCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
