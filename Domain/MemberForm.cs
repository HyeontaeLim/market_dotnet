using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using market.ValidationAtrribute;

namespace market.Domain;

public class MemberForm
{
    public long? MemberId { get; set; }
    [Required(ErrorMessage = "아이디를 입력해주세요.")]
    public string Username { get; set; }
    [Required(ErrorMessage = "비밀번호를 입력해주세요.")]
    public string Password { get; set; }
    [Required(ErrorMessage = "비밀번호 확인을 입력해주세요.")]
    [PasswordConfirm("Password", ErrorMessage = "비밀번호와 비밀번호 확인이 일치하지 않습니다.")]
    public string PasswordConfirm { get; set; }
    [Required(ErrorMessage = "이메일을 입력해주세요.")]
    [EmailAddress(ErrorMessage = "이메일 형식이 아닙니다.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "이름을 입력해주세요.")]
    public string Name { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [Required(ErrorMessage = "성별을 입력해주세요.")]
    public Gender? Gender { get; set; }
    [Required(ErrorMessage = "주소를 입력해주세요.")]
    public string Address { get; set; }

    public MemberForm()
    {
    }

    public MemberForm(long memberId, string username, string password, string passwordConfirm, string email, string name, Gender gender, string address)
    {
        MemberId = memberId;
        Username = username;
        Password = password;
        PasswordConfirm = passwordConfirm;
        Email = email;
        Name = name;
        Gender = gender;
        Address = address;
    }
}