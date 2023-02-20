using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.DTOs.Requests;
using OnlineShop.Interfaces;
using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class AttributeValueService : IAttributeValue
    {
        private DataContext _context;

        public AttributeValueService(DataContext context)
        {
            _context = context;
        }

        public async Task<AttributeValue> AddAttributeValue(AttributeValue attributeValue)
        {
            await _context.AttributeValue.AddAsync(attributeValue);
            await _context.SaveChangesAsync();

            return attributeValue;
        }

        public async Task DeleteAttributeValue(AttributeValue attributeValue)
        {
            _context.AttributeValue.Remove(attributeValue);
            await _context.SaveChangesAsync();
        }

        public async Task<AttributeValue> GetAttributeValueById(int id)
        {
            return await _context.AttributeValue
                .Where(x => x.Id == id)
                .Include(x => x.Attribute)
                .FirstOrDefaultAsync();
        }

        public async Task<List<AttributeValue>> GetAttributeValues()
        {
            return await _context.AttributeValue
                .Include(x => x.Attribute)
                .ToListAsync();
        }

        public async Task<AttributeValue> UpdateAttributeValueOnlyLabel(int id, string label)
        {
            var existingAttributeValue = await _context.AttributeValue.FindAsync(id);
            if (existingAttributeValue != null)
            {
                existingAttributeValue.Label = label;

                _context.AttributeValue.Update(existingAttributeValue);
                await _context.SaveChangesAsync();

                
            }
            return existingAttributeValue;
        }
    }
}
