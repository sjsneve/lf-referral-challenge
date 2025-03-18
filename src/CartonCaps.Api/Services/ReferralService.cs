using CartonCaps.Api.Contexts;
using CartonCaps.Api.Entities;
using CartonCaps.Api.Enums;
using CartonCaps.Api.Interfaces;

namespace CartonCaps.Api.Services;

public class ReferralService : IReferralService
{
    private readonly ILogger<ReferralService> _logger;
    private readonly ReferralDb _context;

    public ReferralService(ILogger<ReferralService> logger, ReferralDb context)
    {
        _logger = logger;
        _context = context;
    }

    public IEnumerable<Referral> GetReferrals()
    {
        return _context.Referrals.ToList();
    }

    public Referral AddReferral(Member member, string referralCode)
    {
        // Check if referral code exists
        var isValidReferralCode = _context.Members.Any(m => m.ReferralCode == referralCode);

        if (!isValidReferralCode)
        {
            throw new Exception($"Invalid Referral code: {referralCode}");
        }

        var newReferral = new Referral
        {
            ReferralCode = referralCode,
            Name = member.FirstName + " " + member.LastName,
            Status = ReferralStatus.Complete
        };
        
        _context.Referrals.Add(newReferral);
        _context.SaveChanges();
        
        return newReferral;
    }

    public IEnumerable<Referral> GetReferralsByReferralCode(string referralCode)
    {
        return _context.Referrals.Where(r => r.ReferralCode == referralCode).ToList();
    }
}