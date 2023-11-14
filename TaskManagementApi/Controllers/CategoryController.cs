using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok( await _categoryRepository.GetAll());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Category? category = await _categoryRepository.GetById(id);
            if (category is not null)
            {
                return Ok(category);
            }

            return NotFound();
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromBody] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category(categoryModel.Name);
                return CreatedAtAction(nameof(AddCategory), _categoryRepository.Add(category));
            }
            return BadRequest();
        }
    }
}
