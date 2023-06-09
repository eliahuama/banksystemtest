using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] string name)
    {
        var userId = _userService.CreateUser(name);

        return Ok(userId);
    }

    [HttpDelete("{userId}")]
    public ActionResult DeleteUser(Guid userId)
    {
        _userService.DeleteUser(userId);

        return NoContent();
    }

    [HttpGet("{userId}")]
    public ActionResult<User> GetUser(Guid userId)
    {
        var user = _userService.GetUser(userId);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}