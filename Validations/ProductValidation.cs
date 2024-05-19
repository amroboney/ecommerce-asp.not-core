using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceAPI.Data.Dto;
using EcommerceAPI.Models;
using FluentValidation;

namespace EcommerceAPI.Validations
{
    public class ProductValidation: AbstractValidator<ProductRequestDto>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name)
            .NotEmpty().WithMessage("the Product name is required")
            .MaximumLength(20);
            

            RuleFor(p => p.Price)
            .NotEmpty().WithMessage("the product price is required");
        }
    }
}