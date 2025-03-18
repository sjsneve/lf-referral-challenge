using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CartonCaps.Api.Enums;

namespace CartonCaps.Api.Entities;

public record Referral
{
    public Referral() { }
    
    public Referral(int id, string referralId, string name, ReferralStatus status)
    {
        Id = id;
        ReferralCode = referralId;
        Name = name;
        Status = status;
    }
    
    public int Id { get; set; }
    
    public string ReferralCode { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;

    public ReferralStatus Status { get; set; }
}