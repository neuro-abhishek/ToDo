using Microsoft.AspNetCore.Mvc;

[Produces("application/json")]
[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    IAuthService _AuthService;
    public AuthController(IAuthService authService)
    {
        _AuthService = authService;
    }

    /// <summary>
    /// Get All TUsers
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
    [HttpPost("signup")]
    public async Task<IActionResult> SignupUser([FromBody] UserDto user)
    {
        try
        {
            string token  = await _AuthService.Signup(user);
            Response<string> res = new Response<string>(token);
            res.StatusCode = 200;
            res.Message = "User Signed up Successfully";
            return Ok(res);
        }
        catch (System.Exception e)
        {
            Console.WriteLine("Error while fetching all users: " + e.Message);
            Response<string> res = new Response<string>("");
            res.StatusCode = 500;
            res.ErrorMessage = e.Message;
            return Ok(res);
        }
    }

    /// <summary>
    /// Create User
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto user)
    {
        try
        {
            string token = await _AuthService.Login(user);
            Response<string> res = new Response<string>(token);
            res.StatusCode = 200;
            res.Message ="User Logged in succcessfully";
            return Ok(res);
        }
        catch (System.Exception e)
        {
            Console.WriteLine("Error while Adding new user: " + e.Message);
            Response<string> res = new Response<string>("");
            res.StatusCode = 500;
            res.ErrorMessage = e.Message;
            return Ok(res);
        }
    }
}









// using Microsoft.AspNetCore.Components;
// using Microsoft.AspNetCore.Mvc;

// [Produces("application/json")]
// [ApiController]
// [Route("/api/auth")]
// public class AuthController : ControllerBase
// {
//     [HttpGet("/signup")]
//     public Task<IActionResult> Signup()
//     {
//         try
//         {

//         }
//         catch (System.Exception e)
//         {
//             throw;
//         }
//     }
// }