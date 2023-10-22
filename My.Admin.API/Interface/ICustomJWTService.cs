﻿using Model.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface ICustomJWTService
    {
        //获取token
        Task<string> GetToken(UserRes user);
    }
}
