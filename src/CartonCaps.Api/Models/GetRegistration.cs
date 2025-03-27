using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CartonCaps.Api.Models;

public class GetRegistration
{
    [Description("The referral code passed to the endpoint as part of registration")]
    [MaxLength(6)]
    [RegularExpression("^[a-zA-Z0-9]{6}$", ErrorMessage = "Referral code must contain only letters and numbers.")]
    public string ReferralCode { get; set; } = string.Empty;

    public bool ShowInvitationBanner => !string.IsNullOrEmpty(ReferralCode);

    public GetRegistration(string? referralCode)
    {
        ReferralCode = referralCode ?? string.Empty;
    }
}