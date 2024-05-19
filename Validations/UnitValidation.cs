using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceAPI.Data.Dto;
using FluentValidation;

namespace EcommerceAPI.Validations
{
    public class UnitValidation: AbstractValidator<UnitRequestDto>
    {
        public UnitValidation()
        {
            RuleFor(u => u.Name )
            .NotEmpty().WithMessage("the unit name is required");
        }
    }
}