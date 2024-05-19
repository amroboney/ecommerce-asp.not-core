using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using EcommerceAPI.Data.Dto;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using EcommerceAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {

        // ILogger takes the type of the class as a parameter
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddressRequestDto> _addressValidator;

        public AddressController(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AddressRequestDto> addressValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addressValidator = addressValidator;
        }


        // create a new Address
        [HttpPost]
        public async Task<IActionResult> CreateUser(AddressRequestDto request)
        {

            // Validate the Address request
            var validationResult = _addressValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                // prepare response
                var validationErrorResponse = ResponseApi.Faild(102 ,$"input errors", validationResult.Errors);
                return Ok(validationErrorResponse);
                // Validation failed, return error messages
                return BadRequest(validationResult.Errors);
            }

            var addressMap = _mapper.Map<Address>(request);
            await _unitOfWork.Address.Add(addressMap); 
            _unitOfWork.Commit();

            var response = ResponseApi.Success($"the {ControllerContext.ActionDescriptor.ControllerName} created Successfly", request);

            return CreatedAtAction(nameof(GetItem), new { id = addressMap.Id }, response);
            
        }

        //get a single Address
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var address = _mapper.Map<AddressRequestDto>(await _unitOfWork.Address.GetById(id));

            if (address == null)
            {
                return NotFound();
            }

            // prepare response
            var response = ResponseApi.Success($"the {ControllerContext.ActionDescriptor.ControllerName} data return Successfly", address);
            return Ok(response);
        }

        //get all Addresses
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var addresses = _mapper.Map<List<AddressRequestDto>>(await _unitOfWork.Address.All());
            if (addresses == null)
            {
                return NotFound();
            }
            
            // prepare response
            var response = ResponseApi.Success($"The {ControllerContext.ActionDescriptor.ControllerName} data return Successfly", addresses);
            return Ok(response);
        }


        //update a Address
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id,[FromBody] AddressRequestDto address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.Address.Update(_mapper.Map<Address>(address));
            _unitOfWork.Commit();

            var response = ResponseApi.Success("the address created Successfly", address);

            return CreatedAtAction(nameof(GetItem), new { id = address.Id }, response);
        }

        //delete a Address
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var address = await _unitOfWork.Address.GetById(id);
            if (address == null)
            {
                return NotFound();
            }

            await _unitOfWork.Address.Delete(id);
            _unitOfWork.Commit();

            // prepare response
            var response = ResponseApi.Success($"The {ControllerContext.ActionDescriptor.ControllerName} Deleted Successfly", _mapper.Map<AddressRequestDto>(address));
            return Ok(response);
        }
    }
}

