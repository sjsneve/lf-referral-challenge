using CartonCaps.Api.Entities;

namespace CartonCaps.Api.Interfaces;

public interface IReferralService
{
    Referral? AddReferral(int memberId, string referralCode);

    IEnumerable<Referral> GetReferralsByReferralCode(string referralCode, int page = 1);
}