using AutoMapper;
using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Responses;
using OnlineShop.Helper;
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

            CreateMap<ProductTypeAttributeValue, ProductTypeAttributeValueResponseDto>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.AttributeValue.Label))
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.AttributeValue.Attribute.Name));

            CreateMap<ProductType, ProductTypeResponseDto>();

            CreateMap<Attribute, AttributeResponseDto>();
        }
    }
}
