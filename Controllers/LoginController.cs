using market.Domain;
using market.Repository;
using market.Validation;
using Microsoft.AspNetCore.Mvc;

namespace market.Controller;

[Microsoft.AspNetCore.Components.Route("[controller]")]
public class LoginController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IAuthService _authService;

    public LoginController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("/login"), Consumes("application/json; charset=UTF-8")]
    public IActionResult Login([FromBody] LoginForm loginForm)
    {
        Member? member = _authService.Login(loginForm.Username, loginForm.Password);
        if (member == null)
        {
            ModelState.AddModelError(nameof(LoginForm), "아이디 또는 비밀번호가 일치하지 않습니다.");
        }
        if (!ModelState.IsValid)
        {
            var validationResult = new ValidationResult(ModelState, loginForm);
            return BadRequest(validationResult);
        }
        HttpContext.Session.SetString("LoginSession", member.MemberId.ToString());
        return Ok();
    }
}