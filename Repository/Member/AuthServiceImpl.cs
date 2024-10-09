using market.Domain;
using Microsoft.AspNetCore.Identity;

namespace market.Repository;

public class AuthServiceImpl : IAuthService
{
    private readonly IMemberRepository _memberRepository;
    private readonly PasswordHasher<string> _passwordHasher;

    public AuthServiceImpl(IMemberRepository memberRepository, PasswordHasher<string> passwordHasher)
    {
        _memberRepository = memberRepository;
        _passwordHasher = passwordHasher;
    }

    public Member? Login(string username, string password)
    {
        var member = _memberRepository.FindByUsername(username);
        if (member == null)
        {
            return null;
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(null, member.Password, password);
        if (passwordVerificationResult != PasswordVerificationResult.Success)
        {
            return null;
        }

        return member;
    }
}