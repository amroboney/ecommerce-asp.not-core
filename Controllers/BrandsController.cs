using System;
using System.Net;
using AutoMapper;
using Azure.Core;
using EcommerceAPI.Data.Dto;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Models;
using EcommerceAPI.Repository;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    public class BrandsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Brands
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetItems()
        {
            var brands =  _mapper.Map<List<BrandDto>>(await _unitOfWork.Brand.All());

            if (brands == null)
            {
                return NotFound();
            }

            // prepare response
            var response = ResponseApi.Success(
                $"The {ControllerContext.ActionDescriptor.ControllerName} data return Successfly",
                brands);
            return Ok(response);
        }

        //GET api/Brands/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Brand))]
        [ProducesResponseType(400)]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var brand = _mapper.Map<BrandDto>(await _unitOfWork.Brand.GetById(id));

            if (brand == null)
            {
                return NotFound();
            }

            // prepare response
            var response = ResponseApi.Success(
                $"the {ControllerContext.ActionDescriptor.ControllerName} data Found Successfly",
                brand);

            return Ok(response);

        }

        // POST api/Brands
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Post([FromBody] CreateBrandDto brandCreate)
        {
            if (ModelState.IsValid)
            {

                var brandMap = _mapper.Map<Brand>(brandCreate);
                await _unitOfWork.Brand.Add(brandMap);
                _unitOfWork.Commit();

                var response = ResponseApi.Success(
                    $"the {ControllerContext.ActionDescriptor.ControllerName} created Successfly",
                    _mapper.Map<BrandDto>(brandMap));

                return CreatedAtAction(nameof(GetItem), new { id = brandMap.Id }, response);
            }

            return BadRequest(ModelState);
        }

        //// PUT api/Brands/5
        [HttpPut("{id}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Put(Guid id, [FromBody] BrandDto brandUpdate)
        {
            if (id != brandUpdate.Id)
            {
                return BadRequest();
            }

            await _unitOfWork.Brand.Update(_mapper.Map<Brand>(brandUpdate));
            _unitOfWork.Commit();

            var response = ResponseApi.Success(
                $"the {ControllerContext.ActionDescriptor.ControllerName} Updated Successfly",
                _mapper.Map<BrandDto>(brandUpdate));

            return CreatedAtAction(nameof(GetItem), new { id = brandUpdate.Id }, response);
        }

        // DELETE api/Brands/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var brand = await _unitOfWork.Brand.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }

            await _unitOfWork.Brand.Delete(id);
            _unitOfWork.Commit();

            // prepare response
            var response = ResponseApi.Success(
                $"The {ControllerContext.ActionDescriptor.ControllerName} Deleted Successfly",
                _mapper.Map<BrandDto>(brand));
            return Ok(response);
        }
    }
}

