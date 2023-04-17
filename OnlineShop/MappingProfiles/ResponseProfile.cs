using AutoMapper;
using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Responses;
using OnlineShop.Helpers;
using OnlineShop.Models;
using Attribute = OnlineShop.Models.Attribute;

namespace OnlineShop.MappingProfiles
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<ProductTypeAttribute, ProductTypeAttributeResponseDto>();

            CreateMap<AttributeValueRequestDto, AttributeValue>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => StringHelper.ConvertToStringKey(src.Label)));
            CreateMap<AttributeValue, AttributeValueResponseDto>();

            CreateMap<ProductType, ProductTypeResponseDto>();

            CreateMap<Attribute, AttributeResponseDto>();

            CreateMap<ProductRequestDto, Product>();//ProductRequstDto has list of ProductAttributeRequestDto
            CreateMap<ProductAttributeRequestDto, ProductAttribute>();

            CreateMap<OrderRequestDto, Order>()
                .ForMember(dest => dest.Processed, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<RegisterRequestDto, User>();
        }
    }
}
