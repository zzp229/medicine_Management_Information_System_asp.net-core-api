using Model.Dto.Login;
using Model.Dto.User;
using Model.Other;

namespace Interface;
public interface IUserService
{
    Task<UserRes> GetUser(LoginReq req);
    Task<UserRes> Get(string id);

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="User"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> Add(UserAdd req, string userId);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="User"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> Edit(UserEdit req, string userId);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Del(string id);
    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    Task<bool> BatchDel(string ids);
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="req"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<PageInfo<UserRes>> GetUsers(UserReq req, string userId);
    /// <summary>
    /// 设置角色
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="rids"></param>
    /// <returns></returns>
    Task<bool> SettingRole(string uid, string rids);
    /// <summary>
    /// 修改昵称和密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="req"></param>
    /// <returns></returns>
    Task<bool> EditNickNameOrPassword(string userId, PersonEdit req);

}

