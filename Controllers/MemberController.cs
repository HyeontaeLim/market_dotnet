using market.Domain;
using market.Filter;
using market.Service;
using market.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace market.Controller;

[Route("[Controller]")]
public class MemberController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IMemberService _memberService;
    private readonly ILogger<MemberController> _logger;
    private readonly PasswordHasher<string> _passwordHasher;

    public MemberController(IMemberService memberService, ILogger<MemberController> logger, PasswordHasher<string> passwordHasher)
    {
        _memberService = memberService;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }
    
    [HttpPost("/register"),Consumes("application/json; charset=UTF-8")]
    public IActionResult MemberSave([FromBody] MemberForm memberForm)
    {
        if (!ModelState.IsValid)
        {
            var validationResult = new ValidationResult(ModelState, memberForm);
            return BadRequest(validationResult);
        }

        var hashPassword = _passwordHasher.HashPassword(null, memberForm.Password);

        Member member = new Member
        {
            Username = memberForm.Username,
            Password = hashPassword,
            Email = memberForm.Email,
            Name = memberForm.Name,
            Gender = memberForm.Gender,
            Address = memberForm.Address
        };
        _memberService.SaveMember(member);
        return Ok();
    }
    
    [HttpGet("/loginmember")]
    [ServiceFilter(typeof(LoginFilter))]
    public IActionResult LoginMember()
    {
        long loginMemberId = long.Parse(HttpContext.Session.GetString("LoginSession"));
        Member? member = _memberService.FindById(loginMemberId);
        return Ok(member);
    }
}