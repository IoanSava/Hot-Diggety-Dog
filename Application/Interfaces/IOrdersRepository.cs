﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{   
    
    public interface IOrdersRepository : IRepository<Order>
    {
        IQueryable<Order> GetAllAsQueryable();

        Task<IEnumerable<Order>> GetByUserId(Guid id);//de fixat dependentele
    }
}
