using NLayerProject.Core.Entities;
using NLayerProject.Core.Repositories;
using NLayerProject.Core.Services;
using NLayerProject.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Category> GetWithProductsByIdAsync(int categoryId)
        {
            return await _unitOfWork.Categories.GetWithProductsByIdAsync(categoryId);
        }

        public async Task<IEnumerable<Category>> GetWithProductsAsync()
        {
            return await _unitOfWork.Categories.GetWithProductsAsync();
        }

        public async Task Remove(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            category.IsDeleted = true;
            await _unitOfWork.CommitAsync();


        }
    }
}
