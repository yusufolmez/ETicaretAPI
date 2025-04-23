using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Application.Validators.ProductImageFile
{
    public class FileValidator : AbstractValidator<IFormFile>
    {
        public FileValidator()
        {
            RuleFor(x => x.Length).LessThanOrEqualTo(5 * 1024 * 1024)
                .WithMessage("File size should not exceed 5MB");
            RuleFor(x => x.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/png") || x.Equals("image/gif"))
                .WithMessage("Only jpeg, png and gif files are allowed");
        }
    }
}
