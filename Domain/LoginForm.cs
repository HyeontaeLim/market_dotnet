using System.ComponentModel.DataAnnotations;

namespace market.Domain;

public class LoginForm
{
    [Required(ErrorMessage = "아이디를 입력해주세요.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "비밀번호를 입력해주세요.")]
    public string Password { get; set; }

    public LoginForm(string username, string password)
    {
        Username = username;
        Password = password;
    }
}