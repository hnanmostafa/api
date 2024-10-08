﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
   public interface IGenericRepository<TEntity, TKey > where TEntity : BaseEntity<TKey>
    {
        Task<TEntity?> GetAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChange);

        Task AddAsync(TEntity entity);
       void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
