using System;
using EcommerceAPI.Data;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EcommerceAPI.Repository
{
	public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly DataContext _context;
        private IDbContextTransaction _transaction;

        
        public UnitOfWork(DataContext context)
		{
            _context = context;
        }

        public IBrandRepository Brand => new BrandRepository(_context);

        public ICategoryRepository Category => new CategoryRepository(_context);

        public IUnitRepository Unit => new UnitRepository(_context);
        
        //public IResponseRepository Response => new ResponseRepository();

        //public IFileRepository FileService { get; private set; }

        public IAddressRepository Address => new AddressRepository(_context);


        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
            //_transaction.Commit();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}

