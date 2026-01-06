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
            await _mongoOperation.PostData(user);
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}