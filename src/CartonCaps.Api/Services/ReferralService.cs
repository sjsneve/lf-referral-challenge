using CartonCaps.Api.Contexts;
using CartonCaps.Api.Entities;
using CartonCaps.Api.Enums;
using CartonCaps.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Referral?> AddReferral(int memberId, string referralCode)
    {
        // Check if referral code exists
        var isValidReferralCode = await _context.Members.AnyAsync(m => m.ReferralCode == referralCode);

        if (!isValidReferralCode)
        {
            return null;
        }

        var newReferral = new Referral (0, referralCode, memberId, ReferralStatus.Complete);
        
        _context.Referrals.Add(newReferral);
        await _context.SaveChangesAsync();
        
        return newReferral;
    }

    public async Task<IEnumerable<Referral>> GetReferralsByReferralCode(string referralCode, int page = 1)
    {
        const int pageSize = 5;
        return await _context.Referrals
            .Include(referral => referral.ReferredMember)
            .Where(r => r.ReferralCode == referralCode)
            .Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }
}