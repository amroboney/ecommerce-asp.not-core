﻿using System;
namespace EcommerceAPI.Interfaces
{
	public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();

        Task<T> GetById(Guid id);

        Task<bool> Add(T entity);

        Task<bool> Delete(Guid id);

        Task<bool> Update(T entity);
    }
}

