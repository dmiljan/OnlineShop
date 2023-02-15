using OnlineShop.DTOs.Requests;
using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface IAttributeValue
    {
        Task<AttributeValue> AddAttributeValue(AttributeValue attributeValue);
        Task<AttributeValue> GetAttributeValueById(int id);
        Task<List<AttributeValue>> GetAttributeValues();
        Task<AttributeValue> UpdateAttributeValueOnlyLabel(int id, string label);
        Task DeleteAttributeValue(AttributeValue attributeValue);
    }
}
