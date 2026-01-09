using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

class UserDal : IUserDal
{
    IMongoOperationDal<UserDto> _mongoOperation;
    public UserDal(IMongoOperationDal<UserDto> mongoOperation)
    {
        _mongoOperation = mongoOperation;
        // _mongoOperation.GetCollectionName("User");
    }
    public async Task<List<UserDto>> ReadUser()
    {
        try
        {
            List<UserDto> res = await _mongoOperation.GetData();
            return res;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public async Task CreateUser(UserDto user)
    {
        try
        {
            var ExistedUserData = await findUserByEmail(user.Email);
            if (ExistedUserData != null)
            {
                throw new Exception("User already exist");
            }
            await _mongoOperation.PostData(user);
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<UserDto> findUserByEmail(string email)
    {
        UserDto user = await _mongoOperation.FindOneAsync(u => u.Email == email);
        return user;
    }
}