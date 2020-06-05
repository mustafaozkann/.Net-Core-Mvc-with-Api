using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.Core.Services;
using Mapster;
using NLayerProject.Web.DTOs;
using NLayerProject.Core.Entities;
using NLayerProject.Web.Filters;
using NLayerProject.Web.ApiServices;
using RestSharp;
using Newtonsoft.Json;

namespace NLayerProject.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryApiService _categoryApiService;

        public CategoriesController(ICategoryService categoryService, CategoryApiService categoryApiService)
        {
            _categoryService = categoryService;
            _categoryApiService = categoryApiService;
        }
        public async Task<IActionResult> Index()
        {
            //var categories = await _categoryService.Where(x => x.IsDeleted == false);
            //var origins = await _categoryApiService.GetAllOrigin();

            var categories = await _categoryApiService.GetAllAsync();
            return View(categories.Adapt<IEnumerable<CategoryDto>>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            // await _categoryService.AddAsync(categoryDto.Adapt<Category>());
            await _categoryApiService.AddAsync(categoryDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //var category = await _categoryService.GetByIdAsync(id);
            var category = await _categoryApiService.GetByIdAsync(id);
            if (category == null)
            {
                ModelState.AddModelError("", "Kategori bulunamadı.");
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            //_categoryService.Update(categoryDto.Adapt<Category>());
            await _categoryApiService.UpdateAsync(categoryDto);
            return RedirectToAction("Index");
        }

        [ServiceFilter(typeof(GenericNotFoundFilter<Category>))]
        public async Task<IActionResult> Delete(int id)
        {
            //var category = _categoryService.GetByIdAsync(id).Result;

            //_categoryService.Remove(id);
            await _categoryApiService.RemoveAsync(id);

            return RedirectToAction("Index");
        }
    }
}