using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FileStrategy
{
    /// <summary>
    /// 工程类，负责创建对象
    /// </summary>
    public  class FileFactory
    {
        public static Strategy CreateStrategy(UploadMode mode)
        {
            switch(mode)
            {
                case UploadMode.Qiniu:
                    return new QiNiuStrategy();
                case UploadMode.Local: 
                    return new LocalStrategy();

                default:
                    return new LocalStrategy();
            }
        }
    }
}
