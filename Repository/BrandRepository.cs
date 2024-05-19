using System;
using System.Linq;
using EcommerceAPI.Data;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repository
{
	public class BrandRepository: GenericRepository<Brand>, IBrandRepository
	{

        public BrandRepository(DataContext context) : base(context)
        {
        }   
        

    }
}

