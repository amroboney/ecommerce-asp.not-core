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
    public class UnitController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UnitRequestDto> _unitValidator;
        public UnitController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UnitRequestDto> unitValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitValidator = unitValidator;
        }

        // create unit
        [HttpPost]
        public async Task<IActionResult> create(UnitRequestDto request)
        {
            var validationResult = _unitValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var validationErrorResponse = ResponseApi.Faild(102, "input errors", validationResult.Errors);
                return Ok(validationErrorResponse);
            }

            var unitMap = _mapper.Map<Unit>(request);
            await _unitOfWork.Unit.Add(unitMap);
            _unitOfWork.Commit();

            var response = ResponseApi.Success($"the {ControllerContext.ActionDescriptor.ControllerName} created Successfly", request);

            return CreatedAtAction(nameof(GetItem), new { id = unitMap.Id }, response);
            
        }


        //get a single unit
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var unit = _mapper.Map<UnitRequestDto>(await _unitOfWork.Unit.GetById(id));

            if (unit == null)
            {
                return NotFound();
            }

            // prepare response
            var response = ResponseApi.Success($"the {ControllerContext.ActionDescriptor.ControllerName} data return Successfly", unit);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetItems() 
        {
            // return Ok(await _unitOfWork.Unit.All());
            var units = _mapper.Map<List<UnitRequestDto>>(await _unitOfWork.Unit.All());
            if (units == null)
                return NotFound();

            var response = ResponseApi.Success($"The {ControllerContext.ActionDescriptor.ControllerName} data return Successfly", units);
            return Ok(response);
            
        }
    }
}