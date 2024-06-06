using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataBase;
using DataBase.Models;

namespace DataBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressDBModelsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AddressDBModelsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/AddressDBModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDBModel>>> GetAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/AddressDBModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDBModel>> GetAddressDBModel(Guid id)
        {
            var addressDBModel = await _context.Addresses.FindAsync(id);

            if (addressDBModel == null)
            {
                return NotFound();
            }

            return addressDBModel;
        }

        // PUT: api/AddressDBModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddressDBModel(Guid id, [FromBody]AddressDBModel addressDBModel)
        {
            if (id != addressDBModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(addressDBModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressDBModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/AddressDBModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AddressDBModel>> PostAddressDBModel(AddressDBModel addressDBModel)
        {
            _context.Addresses.Add(addressDBModel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/AddressDBModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressDBModel(Guid id)
        {
            var addressDBModel = await _context.Addresses.FindAsync(id);
            if (addressDBModel == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(addressDBModel);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AddressDBModelExists(Guid id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
