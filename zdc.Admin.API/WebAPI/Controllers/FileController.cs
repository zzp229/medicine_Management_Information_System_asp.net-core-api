using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Enum;
using Model.Other;
using WebAPI.Config;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService) { 
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<ApiResult> UploadFile(List<IFormFile>file,UploadMode mode)
        {
            return ResultHelper.Success(await _fileService.Upload(file, mode));
        }
    }
}
