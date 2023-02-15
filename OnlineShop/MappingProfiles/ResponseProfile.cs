using AutoMapper;
using OnlineShop.DTOs.Requests;
using OnlineShop.DTOs.Responses;
using OnlineShop.Models;
using Attribute = OnlineShop.Models.Attribute;

namespace OnlineShop.MappingProfiles
{
    public class ResponseProfile : Profile
    {
        public ResponseProfile()
        {
            CreateMap<ProductTypeAttribute, ProductTypeAttributeResponseDto>();
                //.ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.Attribute.Name));

            CreateMap<AttributeValueRequestDto, AttributeValue>();
            CreateMap<AttributeValue, AttributeValueResponseDto>();
            //.ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.Attribute.Name));

            CreateMap<ProductTypeAttributeValue, ProductTypeAttributeValueResponseDto>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.AttributeValue.Label))
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.AttributeValue.Attribute.Name));

            CreateMap<ProductType, ProductTypeResponseDto>();

            CreateMap<Attribute, AttributeResponseDto>();
        }
    }
}
