using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceAPI.Data;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;

namespace EcommerceAPI.Repository
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        public UnitRepository(DataContext context) : base(context)
        {
        }
    }
}