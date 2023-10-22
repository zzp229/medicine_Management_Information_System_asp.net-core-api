using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Model.Enum;

namespace Interface
{
    public  interface IFileService
    {
        Task<string> Upload(List<IFormFile> files, UploadMode mode);
    }
}
