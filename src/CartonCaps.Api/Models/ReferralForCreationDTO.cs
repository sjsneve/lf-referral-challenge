using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CartonCaps.Api.Models;

public class CreateReferralDTO
{
    [Description("The referral code")]
    [Required]
    [RegularExpression("^[A-Za-z0-9]{6}$", ErrorMessage = "Referral code must be 6 characters long and contain only letters.")]
    public string? ReferralCode { get; set; } = string.Empty;
}