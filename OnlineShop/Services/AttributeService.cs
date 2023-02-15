using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Interfaces;
using Attribute = OnlineShop.Models.Attribute;

namespace OnlineShop.Services
{
    public class AttributeService : IAttribute
    {
        private DataContext _context;

        public AttributeService(DataContext context)
        {
            _context = context;
        }

        public async Task<Attribute> AddAttribute(Attribute attribute)
        {
            await _context.Attribute.AddAsync(attribute);
            await _context.SaveChangesAsync();

            return attribute;
        }

        public async Task DeleteAttribute(Attribute attribute)
        {
            _context.Attribute.Remove(attribute);
            await _context.SaveChangesAsync();
        }

        public async Task<Attribute> GetAttributeById(int id)
        {
            var attribute = await _context.Attribute.FindAsync(id);
            return attribute;
        }

        public async Task<List<Attribute>> GetAttributes()
        {
            var attributes = await _context.Attribute.ToListAsync();
            return attributes;
        }

        public async Task UpdateAttribute(Attribute attribute)
        {
            var existingAttribute = await _context.Attribute.FindAsync(attribute.Id);
            if(existingAttribute != null)
            {
                existingAttribute.Name = attribute.Name;
                existingAttribute.ValueType = attribute.ValueType;
                existingAttribute.InputType = attribute.InputType;

                _context.Attribute.Update(existingAttribute);
                await _context.SaveChangesAsync();
            }
        }
    }
}
