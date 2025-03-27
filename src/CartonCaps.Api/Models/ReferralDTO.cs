using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CartonCaps.Api.Enums;

namespace CartonCaps.Api.Models;

public class ReferralDTO
{
    
    [Description("The referred users name")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Description("The status of the referral (i.e. Completed, Pending)")]
    public ReferralStatus Status { get; set; }
}