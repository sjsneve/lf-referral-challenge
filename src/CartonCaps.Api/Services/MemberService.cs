using CartonCaps.Api.Contexts;
using CartonCaps.Api.Entities;
using CartonCaps.Api.Interfaces;

namespace CartonCaps.Api.Services;

public class MemberService : IMemberService
{
    private readonly ILogger<MemberService> _logger;
    private readonly ReferralDb _context;

    public MemberService(ILogger<MemberService> logger, ReferralDb context)
    {
        _logger = logger;
        _context = context;
    }

    public Member GetMember(int id)
    {
        var member = _context.Members.SingleOrDefault(m => m.Id == id);

        if (member == null)
        {
            throw new Exception($"Member with id {id} not found");
        }
        
        return member;
    }
}