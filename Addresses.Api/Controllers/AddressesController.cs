using Addresses.Services.Abstractions.Service;
using Addresses.Services.Models.Address;
using Microsoft.AspNetCore.Mvc;

namespace Addresses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesQueryService _queryService;
        private readonly IAddressesCommandService _commandService;

        public AddressesController(IAddressesQueryService queryService, IAddressesCommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        // GET /addresses
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] string search, [FromQuery] string sortBy, [FromQuery] bool sortDesc)
        {
            var addresses = await _queryService.GetAllAsync(search, sortBy, sortDesc);
            return Ok(addresses);
        }

        // GET /addresses/:id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var address = await _queryService.GetByIdAsync(id);
                return Ok(address);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST /addresses
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddressCreate address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAddress = await _commandService.CreateAsync(address);
            return CreatedAtAction(nameof(GetById), new { id = newAddress.Id }, newAddress);
        }

        // PUT /addresses/:id
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] AddressUpdate address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedAddress = await _commandService.UpdateAsync(id, address);
                return Ok(updatedAddress);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE /addresses/:id
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _commandService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id1}/{id2}/distance")]
        public async Task<IActionResult> GetDistance(int id1, int id2)
        {
            try
            {
                var distance = await _queryService.GetDistanceAsync(id1, id2);
                return Ok(distance);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
