using System;
using EcommerceAPI.Data.Dto;
using EcommerceAPI.Models;
using FluentValidation;

namespace EcommerceAPI.Validations
{
    public class AddressValidation : AbstractValidator<AddressRequestDto>
    {
        public AddressValidation()
        {
            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("the name is requires and not empty")
                .MaximumLength(20).WithMessage("the name must be less than 20 digits");    
        }
    }
}

