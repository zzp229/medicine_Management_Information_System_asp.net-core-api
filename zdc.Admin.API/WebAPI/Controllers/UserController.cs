using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.User;
using Model.Other;
using WebAPI.Config;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _User;
        public UserController(IUserService User)
        {
            _User = User;
        }
        [HttpPost]
        public async Task<ApiResult> Add(UserAdd req)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _User.Add(req, userId));
        }
        [HttpPost]
        public async Task<ApiResult> Edit(UserEdit req)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _User.Edit(req, userId));
        }
        [HttpGet]
        public async Task<ApiResult> Del(string id)
        {
            return ResultHelper.Success(await _User.Del(id));
        }
        [HttpGet]
        public async Task<ApiResult> BatchDel(string ids)
        {
            return ResultHelper.Success(await _User.BatchDel(ids));
        }
        [HttpPost]
        public async Task<ApiResult> GetUsers(UserReq req)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _User.GetUsers(req, userId));
        }
        [HttpGet]
        public async Task<ApiResult> SettingRole(string uid, string rids)
        {
            return ResultHelper.Success(await _User.SettingRole(uid, rids));
        }
        [HttpPost]
        public async Task<ApiResult> EditNickNameOrPassword(PersonEdit req)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _User.EditNickNameOrPassword(userId, req));
        }
    }
}

