using CartonCaps.Api.Entities;
using CartonCaps.Api.Models;

namespace CartonCaps.Api.Mappers;

public static class ReferralMapper
{
    public static ReferralDTO ToReferralDTO(this Referral referral)
    {
        return new ReferralDTO
        {
            Name = $"{referral.ReferredMember.FirstName} {referral.ReferredMember.LastName}",
            Status = referral.Status
        };
    }
}