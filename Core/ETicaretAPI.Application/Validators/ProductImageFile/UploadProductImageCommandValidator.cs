using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Validators.ProductImageFile
{
    public class UploadProductImageCommandValidator : AbstractValidator<UploadProductImageCommandRequest>
    {
        public UploadProductImageCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required");
            RuleFor(x => x.Files).NotEmpty().WithMessage("At least one file must be uploaded");
            RuleForEach(x => x.Files).SetValidator(new FileValidator());
        }
    }
}
