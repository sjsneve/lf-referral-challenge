using CartonCaps.Api.Entities;

namespace CartonCaps.Api.Interfaces;

public interface IReferralService
{
    Task<Referral?> AddReferral(int memberId, string referralCode);

    Task<IEnumerable<Referral>> GetReferralsByReferralCode(string referralCode, int page = 1);
}