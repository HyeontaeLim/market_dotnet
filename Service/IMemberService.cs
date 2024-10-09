using market.Domain;

namespace market.Service;

public interface IMemberService
{
    Member SaveMember(Member member);
    IEnumerable<Member> FindMembersAll();
    Member? FindByUsername(string username);
    Member? FindById(long id);
    void DeleteById(long id);
    Member UpdateById(Member member, long id);
}