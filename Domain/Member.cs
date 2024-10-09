using System.Text.Json.Serialization;

namespace market.Domain;

public class Member
{
    public long? MemberId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender? Gender { get; set; }
    public string Address { get; set; }

    public Member()
    {
    }

    public Member(long memberId, string username, string password, string email, string name, Gender gender, string address)
    {
        MemberId = memberId;
        Username = username;
        Password = password;
        Email = email;
        Name = name;
        Gender = gender;
        Address = address;
    }
}