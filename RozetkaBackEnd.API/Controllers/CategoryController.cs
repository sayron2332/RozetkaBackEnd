using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RozetkaBackEnd.Core.Dtos.Category;
using RozetkaBackEnd.Core.Entites.Category;
using RozetkaBackEnd.Core.Services;
using RozetkaBackEnd.Core.Validations.Category;

namespace RozetkaBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService) 
        { 
            _categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(CategoryDto category)
        {
            CategoryValidation validations = new CategoryValidation();
            var validationResult = validations.Validate(category);
            if (validationResult.IsValid )
            {
                var result = await _categoryService.Create(category);
                return Ok(result);
            }
            return BadRequest(validationResult.Errors[0]);

        }

        [HttpGet("Get")]
        public async Task<ActionResult> Get(int Id)
        {
          
           
          var result = await _categoryService.Get(Id);
          if (result.Success)
          {
              return Ok(result);
          }
        
        
           return BadRequest(result.Message);

        }
    }
}
