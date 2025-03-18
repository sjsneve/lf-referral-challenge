using CartonCaps.Api.Entities;

namespace CartonCaps.Api.Interfaces;

public interface IReferralService
{
    Referral AddReferral(Member member, string referralCode);

    IEnumerable<Referral> GetReferralsByReferralCode(string referralCode);
}