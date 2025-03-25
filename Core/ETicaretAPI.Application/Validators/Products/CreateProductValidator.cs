using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Name is required")
                .MaximumLength(150)
                .MinimumLength(3)
                    .WithMessage("Name must be between 3 and 150 characters");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Stock is required")
                .GreaterThan(0)
                    .WithMessage("Stock must be greater than 0");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Price is required")
                .GreaterThan(0)
                    .WithMessage("Price must be greater than 0");
        }
    }
}
