using Deli.Helpers;
using Deli.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Services;

namespace Deli.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryServices _categoryServices;

        public CategoriesController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll([FromQuery] CategoryFilter filter) => Ok(await _categoryServices.GetAll(filter) , filter.PageNumber , filter.PageSize);

        
        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] CategoryForm categoryForm) => Ok(await _categoryServices.Create(categoryForm));

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Update([FromBody] CategoryUpdate categoryUpdate, Guid id) => Ok(await _categoryServices.Update(id , categoryUpdate));

        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(Guid id) =>  Ok( await _categoryServices.Delete(id));
        
    }
}
