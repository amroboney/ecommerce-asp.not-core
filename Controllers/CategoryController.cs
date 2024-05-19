using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using EcommerceAPI.Data.Dto;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileService;
        
        private readonly string FolderName = "Category";

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = _mapper.Map<List<CategoryDto>>(await _unitOfWork.Category.All());

            var newCategories = categories.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                // Construct the full image path based on the base URL and image name
                ImagePath = _fileService.GetImage(p.Image, FolderName)
            });

            // prepare response
            var response = ResponseApi.Success(
                $"The {ControllerContext.ActionDescriptor.ControllerName} data return Successfly",
                newCategories);
            return Ok(response);
        }

        // GET: api/category
        [HttpGet]
        [Route("Active")]
        public IActionResult GetActive()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_unitOfWork.Category.getActive()) ;

            var newCategories = categories.Select(p => new
            {
                Id = p.Id,
                Name = p.Name,
                // Construct the full image path based on the base URL and image name
                ImagePath = _fileService.GetImage(p.Image, FolderName)
            });

            // prepare response
            var response = ResponseApi.Success(
                $"The {ControllerContext.ActionDescriptor.ControllerName} data return Successfly",
                newCategories);
            return Ok(response);
        }


        // GET api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var category = await _unitOfWork.Category.GetById(id);

            if (category == null)
                return NotFound();
            var fullPathImage = _fileService.GetImage(category.Image, FolderName);
            
            category.Image = fullPathImage;
            // prepare response
            var response = ResponseApi.Success(
                $"the {ControllerContext.ActionDescriptor.ControllerName} data Found Successfly",
                _mapper.Map<CategoryDto>(category));

            return Ok(response);
        }

        //// POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateCategoryDto request)
        {
            if (!ModelState.IsValid)
                return Ok(ModelState);
            

            if (request.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(request.ImageFile, FolderName);

                if (fileResult.Item1 == 1)
                    request.Image = fileResult.Item2;
            }

            var CategoryMap = _mapper.Map<Category>(request);
            
            var categoryResult = await _unitOfWork.Category.Add(CategoryMap);
            _unitOfWork.Commit();

            CategoryMap.Image = _fileService.GetImage(CategoryMap.Image, FolderName);
            // prepare response
            var response = ResponseApi.Success(
                $"the {ControllerContext.ActionDescriptor.ControllerName} data Found Successfly",
                _mapper.Map<CategoryDto>(CategoryMap));

            return Ok(response);
            
            
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var category = await _unitOfWork.Category.GetById(id);
            if (category == null)
                return NotFound();

            var resutl = await _unitOfWork.Category.Delete(id);
            _unitOfWork.Commit();

            if(resutl)
                _fileService.DeleteImage(category.Image, FolderName);

            // prepare response
            var response = ResponseApi.Success(
                $"The {ControllerContext.ActionDescriptor.ControllerName} Deleted Successfly",
                _mapper.Map<CategoryDto>(category));
            return Ok(response);
        }
    }
}

