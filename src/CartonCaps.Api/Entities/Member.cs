namespace CartonCaps.Api.Entities;

public record Member
{
    public Member() { }
    
    public Member(int id, string firstName, string lastName, string referralCode)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        ReferralCode = referralCode;
    }
    
    
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string ReferralCode { get; set; } = string.Empty;
}