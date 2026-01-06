public interface IAuthService
{
    public Task<string> Signup(UserDto user); 
    public Task<string> Login(UserDto user);
}