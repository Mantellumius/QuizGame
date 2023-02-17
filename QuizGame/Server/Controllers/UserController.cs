namespace QuizGame.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    // yourdomain.com/api/user

    public UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    // yourdomain.com/api/user/register
    [Route("register")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        var username = user.EmailAddress;
        var password = user.Password;

        var identityUser = new IdentityUser
        {
            Email = username,
            UserName = username
        };

        var userIdentityResult = await _userManager.CreateAsync(identityUser, password);

        if (userIdentityResult.Succeeded)
            return Ok(new { userIdentityResult.Succeeded });

        var errorsToReturn = "Register failed with the following errors";

        foreach (var error in userIdentityResult.Errors)
        {
            errorsToReturn += Environment.NewLine;
            errorsToReturn += $"Error code: {error.Code} - {error.Description}";
        }

        return StatusCode(StatusCodes.Status500InternalServerError, errorsToReturn);
    }

    [Route("signin")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> SignIn([FromBody] User user)
    {
        var username = user.EmailAddress;
        var password = user.Password;

        var signInResult = await _signInManager.PasswordSignInAsync(username, password, false, false);

        if (!signInResult.Succeeded) return Unauthorized(user);
        var identityUser = await _userManager.FindByNameAsync(username);
        var JSONWebTokenAsString = await GenerateJsonWebToken(identityUser);
        return Ok(JSONWebTokenAsString);

    }

    [NonAction]
    [ApiExplorerSettings(IgnoreApi = true)]
    private async Task<string> GenerateJsonWebToken(IdentityUser identityUser)
    {
        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        // Claim = who is the person trying to sign in claming to be?
        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
        };

        var roleNames = await _userManager.GetRolesAsync(identityUser);
        claims.AddRange(roleNames.Select(roleName => new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)));

        var jwtSecurityToken = new JwtSecurityToken
        (
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            null,
            expires: DateTime.UtcNow.AddDays(28),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
