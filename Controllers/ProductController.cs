using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EcommerceAPI.Data.Dto;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;
using EcommerceAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileService;
        private readonly IValidator<ProductRequestDto> _productValidator;

        private readonly string FolderName = "Product";
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IFileRepository fileService, IValidator<ProductRequestDto> productValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
            _productValidator = productValidator;
        }


        //// POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductRequestDto request)
        {
            var validationResult = _productValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                // prepare response
                var validationErrorResponse = ResponseApi.Success($"input errors", validationResult.Errors);
                return Ok(validationErrorResponse);
            }
            

            if (request.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(request.ImageFile, FolderName);

                if (fileResult.Item1 == 1)
                    request.Image = fileResult.Item2;
            }

            var productMap = _mapper.Map<Category>(request);
            
            var categoryResult = await _unitOfWork.Category.Add(productMap);
            _unitOfWork.Commit();

            productMap.Image = _fileService.GetImage(productMap.Image, FolderName);
            // prepare response
            var response = ResponseApi.Success(
                $"the {ControllerContext.ActionDescriptor.ControllerName} data Found Successfly",
                _mapper.Map<CategoryDto>(productMap));

            return Ok(response);
            
            
        }

    }
}