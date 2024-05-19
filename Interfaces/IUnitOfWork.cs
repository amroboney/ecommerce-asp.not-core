using System;
namespace EcommerceAPI.Interfaces
{
	public interface IUnitOfWork
    {
        void BeginTransaction();

        void Commit();

        void Rollback();


        IBrandRepository Brand { get; }

        IAddressRepository Address { get; }

        ICategoryRepository Category{ get; }

        //IResponseRepository Response { get; }

        //IFileRepository FileService { get; }
    }
}

