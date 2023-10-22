using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FileStrategy
{
    /// <summary>
    /// 本地上传，写具体逻辑的地方
    /// </summary>
    public class LocalStrategy : Strategy
    {
        public override async Task<string> Upload(List<IFormFile> files)
        {
            var result = Task.Run(() => {
                List<string> res = new List<string>();
                foreach (var formFile in files)
                {
                    if(formFile.Length>0)
                    {
                        var filePath = $"{AppContext.BaseDirectory}/wwwroot";
                        var fileName = $"/{DateTime.Now:yyyyMMddHHmmssffff}{formFile.FileName}";
                        if(!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        using (var stream = File.Create(filePath+fileName))
                        {
                            formFile.CopyTo(stream);
                        }
                        res.Add(fileName);
                    }
                }
                // 将路径数组格式化成逗号分割的字符串
                return string.Join(",",res);
            });
            return await result;
        }
    }
}
