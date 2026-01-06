using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

class AuthService : IAuthService
{
    private readonly IUserDal _UserDal;
    private readonly IConfiguration _config;
    public AuthService(IUserDal userDal, IConfiguration config)
    {
        _UserDal = userDal;
        _config = config;
    }

    public string CreateToken(UserDto payLoad)
    {
        try
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, payLoad.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenObject = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_config["Jwt:DurationInMinutes"])
                )
            );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return tokenString;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> Signup(UserDto user)
    {
        try
        {
            await _UserDal.CreateUser(user);
            string token = CreateToken(user);
            Console.WriteLine(token);
            return token;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> Login(UserDto user)
    {
        try
        {
            string token = CreateToken(user);
            return token;
        }
        catch (System.Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}