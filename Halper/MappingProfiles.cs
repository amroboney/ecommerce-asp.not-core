using System;
using AutoMapper;
using EcommerceAPI.Data.Dto;
using EcommerceAPI.Models;

namespace EcommerceAPI.Halper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Brand, BrandDto>().ReverseMap();
			CreateMap<CreateCategoryDto, Category>().ReverseMap();
			CreateMap<AddressRequestDto, Address>().ReverseMap();
			CreateMap<CreateBrandDto, Brand>().ReverseMap();
			CreateMap<BrandDto, CreateBrandDto>().ReverseMap();
			CreateMap<CategoryDto, Category>().ReverseMap();
		}
	}
}

