using market.Domain;
using market.Repository;

namespace market.Service;

public class MemberServiceImpl : IMemberService
{
    private IMemberRepository _memberRepository;

    public MemberServiceImpl(IMemberRepository memberRepository)
    
    {
        _memberRepository = memberRepository;
    }

    public Member SaveMember(Member member)
    {
        if (member == null)
        {
            throw new ArgumentNullException(nameof(member), "member object cannot be null");
        }
        return _memberRepository.SaveMember(member);
    }

    public IEnumerable<Member> FindMembersAll()
    {
        return _memberRepository.FindMembersAll();
    }

    public Member? FindByUsername(string username)
    {
        return _memberRepository.FindByUsername(username);
    }

    public Member? FindById(long id)
    {
        return _memberRepository.FindById(id);
    }

    public void DeleteById(long id)
    {
        _memberRepository.DeleteById(id);
    }

    public Member UpdateById(Member member, long id)
    {
        throw new NotImplementedException();
    }
}