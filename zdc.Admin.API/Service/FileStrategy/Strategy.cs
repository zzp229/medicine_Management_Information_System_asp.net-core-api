using Microsoft.AspNetCore.Http;

namespace Service.FileStrategy
{
    /// <summary>
    /// 文件操作抽象类
    /// </summary>
    public abstract class Strategy
    {
        /// <summary>
        /// 上传方法
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public abstract Task<string> Upload(List<IFormFile> files);
    }
}
