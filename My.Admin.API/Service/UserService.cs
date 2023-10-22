using Interface;
using Model.Dto.Login;
using Model.Dto.User;
using Model.Entitys;
using SqlSugar;
namespace Service;
public class UserService : IUserService
{
    private ISqlSugarClient _db { get; set; }

    public UserService(ISqlSugarClient db)
    {
        _db = db;
    }
    /// <summary>
    ///  登录生成token用
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    public async Task<UserRes> GetUser(LoginReq req)
    {
        return await _db.Queryable<Users>().Where(u => u.Name == req.UserName && u.Password == req.PassWord)
                .Select(s => new UserRes() { },true).FirstAsync();
    }

    public async Task<UserRes> Get(string id)
    {
        return await _db.Queryable<Users>().Where(p => p.Id == id).Select(s => new UserRes() { }, true).FirstAsync();
    }
}

