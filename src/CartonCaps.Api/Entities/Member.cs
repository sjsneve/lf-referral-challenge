using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CartonCaps.Api.Entities;

public record class Member : BaseEntity
{
    public Member() { }
    
    [SetsRequiredMembers]
    public Member(int id, string firstName, string lastName, string referralCode)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ReferralCode = referralCode;
    }

    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public required string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(6)]
    public required string ReferralCode { get; set; } = string.Empty;
}