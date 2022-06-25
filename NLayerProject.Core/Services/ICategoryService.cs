﻿using NLayerProject.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<Category> GetWithProductsByIdAsync(int categoryId);
        Task<IEnumerable<Category>> GetWithProductsAsync();
        Task Remove(int id);
    }
}
