using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Menu;
using Model.Entitys;
using SqlSugar;

namespace WebAPI.Controllers
{
    // api/tool/方法名
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToolController : ControllerBase
    {
        private readonly ILogger<ToolController> _logger;
        private readonly ISqlSugarClient _db;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="db"></param>
        public ToolController(ISqlSugarClient db, ILogger<ToolController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<bool> CodeFirst()
        {
            // 1、生成数据库
            _db.DbMaintenance.CreateDatabase();
            var tblist = _db.DbMaintenance.GetTableInfoList();
            if (tblist != null && tblist.Count > 0)
            {
                tblist.ForEach(t =>
                {
                    _db.DbMaintenance.DropTable(t.Name);
                });
            }
            // 2、生成表
            string nspace = "Model.Entitys";
            Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
            // SetStringDefaultLength 为所有字符串类型的属性指定一个默认长度（默认是100，这里改为200，Model里会覆盖这里）
            _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
            // 3、添加测试数据
            //初始化炒鸡管理员和菜单
            Users user = new Users()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NickName = "炒鸡管理员",
                Password = "123456",
                UserType = 0,
                IsEnable = true,
                Description = "数据库初始化时默认添加的炒鸡管理员",
                CreateDate = DateTime.Now,
                CreateUserId = 0,
            };
            long userId = await _db.Insertable(user).ExecuteReturnBigIdentityAsync();
            var m1 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "菜单管理",
                Index = "/menu",
                FilePath = "menu.vue",
                ParentId = 0,
                Order = 1,
                IsEnable = true,
                Icon = "folder",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            var bigid = await _db.Insertable(m1).ExecuteReturnBigIdentityAsync();
            var m2 = new Menu()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "菜单列表",
                Index = "/menu",
                FilePath = "menu.vue",
                ParentId = bigid,
                Order = 1,
                IsEnable = true,
                Icon = "notebook",
                Description = "数据库初始化时默认添加的默认菜单",
                CreateDate = DateTime.Now,
                CreateUserId = userId
            };
            long id = await _db.Insertable(m2).ExecuteCommandAsync();
            return id > 0;
        }
    }
}