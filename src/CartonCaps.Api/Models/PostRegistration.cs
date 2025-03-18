using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CartonCaps.Api.Models;

public class PostRegistration
{
    [Description("The users name")]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Z\'\s]+$", ErrorMessage = "Name must contain only letters.")]
    public string FirstName { get; set; } = string.Empty;

    [Description("The users last name")]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Z\'\s]+$", ErrorMessage = "Name must contain only letters.")]
    public string LastName { get; set; } = string.Empty;
    
    [Description("The referral code")]
    [MaxLength(6)]
    public string ReferralCode { get; set; } = string.Empty;
    
    [Description("The users date of birth")]
    public DateTime DateOfBirth { get; set; }

    [Description("The users zip code")]
    [MaxLength(5)]
    [RegularExpression(@"^\d{5}$", ErrorMessage = "Zip code must be 5 digits.")]
    public string ZipCode { get; set; } = string.Empty;
}