using OnlineShop.Models;
using Attribute = OnlineShop.Models.Attribute;

namespace OnlineShop.Interfaces
{
    public interface IAttribute
    {
        Task<List<Attribute>> GetAttributes();
        Task<Attribute> GetAttributeById(int id);
        Task<Attribute> AddAttribute(Attribute attribute);
        Task UpdateAttribute(Attribute attribute);
        Task DeleteAttribute(Attribute attribute);
    }
}
