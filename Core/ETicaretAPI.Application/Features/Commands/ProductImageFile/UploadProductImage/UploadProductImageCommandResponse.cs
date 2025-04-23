using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> UploadedFileNames { get; set; } = new();
        public List<string> Errors { get; set; } = new();
    }
}
