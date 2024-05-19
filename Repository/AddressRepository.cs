
using System;
using EcommerceAPI.Data;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;
using Microsoft.Extensions.Logging;

namespace EcommerceAPI.Repository
{
    public class AddressRepository: GenericRepository<Address>, IAddressRepository
    {
        //private DataContext context;

        public AddressRepository(DataContext context) : base(context)
        {
            //this.context = context;
        }
    }
}

