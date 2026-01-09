public interface IUserDal
{
    public Task<List<UserDto>> ReadUser();
    public Task CreateUser(UserDto user);

    public Task<UserDto> findUserByEmail(string email);
}