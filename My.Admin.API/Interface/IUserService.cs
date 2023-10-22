using Model.Dto.Login;
using Model.Dto.User;

namespace Interface;
public interface IUserService
{
    Task<UserRes> GetUser(LoginReq req);
    Task<UserRes> Get(string id);
}

