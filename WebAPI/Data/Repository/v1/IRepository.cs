﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data.Common;

namespace WebAPI.Data.Repository.v1
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<bool> ExistsAsync(Guid id);
    }
}