using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Role;
using Model.Other;
using WebAPI.Config;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _Role;
        public RoleController(IRoleService Role)
        {
            _Role = Role;
        }
        [HttpPost]
        public async Task<ApiResult> Add(RoleAdd req)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _Role.Add(req, userId));
        }
        [HttpPost]
        public async Task<ApiResult> Edit(RoleEdit req)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _Role.Edit(req, userId));
        }
        [HttpGet]
        public async Task<ApiResult> Del(string id)
        {
            return ResultHelper.Success(await _Role.Del(id));
        }
        [HttpGet]
        public async Task<ApiResult> BatchDel(string ids)
        {
            return ResultHelper.Success(await _Role.BatchDel(ids));
        }
        [HttpPost]
        public async Task<ApiResult> GetRoles(RoleReq req)
        {
            userId = HttpContext.User.Claims.ToList()[0].Value;
            return ResultHelper.Success(await _Role.GetRoles(req, userId));
        }
    }
}

