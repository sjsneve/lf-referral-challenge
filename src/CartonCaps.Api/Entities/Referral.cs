using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using CartonCaps.Api.Enums;

namespace CartonCaps.Api.Entities;

public record class Referral : BaseEntity
{
    public Referral() { }
    
    [SetsRequiredMembers]
    public Referral(int id, string referralCode, int memberId, ReferralStatus status)
    {
        Id = id;
        ReferralCode = referralCode;
        ReferredMemberId = memberId;
        Status = status;
    }
    
    [Required]
    [MaxLength(6)]
    public required string ReferralCode { get; set; } = string.Empty;
    
    [Required]
    public required int ReferredMemberId { get; set; }
    
    [ForeignKey(nameof(ReferredMemberId))]
    public Member ReferredMember { get; set; } = null!;
    
    [Required]
    public required ReferralStatus Status { get; set; }

}