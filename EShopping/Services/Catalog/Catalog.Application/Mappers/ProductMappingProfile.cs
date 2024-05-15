using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Dto;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Brands,BrandDto>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Types, TypeDto>().ReverseMap();
            CreateMap<Pagination<Product>, Pagination<ProductDto>>().ReverseMap();
        }
    }
}
