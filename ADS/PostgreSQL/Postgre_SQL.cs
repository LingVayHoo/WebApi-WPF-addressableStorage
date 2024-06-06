using DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.PostgreSQL
{
    public class Postgre_SQL
    {
        private PostgreSQLContext _context;

        public Postgre_SQL()
        {
            _context = new();
        }

        public async Task<ActionResult<IEnumerable<AddressDBModel>>> GetAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<ActionResult<AddressDBModel>?> GetAddressDBModel(Guid id)
        {
            var addressDBModel = await _context.Addresses.FindAsync(id);

            if (addressDBModel == null)
            {
                return null;
            }

            return addressDBModel;
        }

        public async Task<bool> PutAddressDBModel(Guid id, AddressDBModel addressDBModel)
        {
            if (id != addressDBModel.Id)
            {
                return false;
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
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> PostAddressDBModel(AddressDBModel addressDBModel)
        {
            _context.Addresses.Add(addressDBModel);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAddressDBModel(Guid id)
        {
            var addressDBModel = await _context.Addresses.FindAsync(id);
            if (addressDBModel == null)
            {
                return false;
            }

            _context.Addresses.Remove(addressDBModel);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool AddressDBModelExists(Guid id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
