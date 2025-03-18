using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CartonCaps.Api.Models;

public class InviteFriendsModel
{
    [Description("The description of the invite friends page")]
    [MaxLength(100)]
    public string Description { get; set; }
    
    [Description("Referral code used to refer friends to the application.")]
    [MaxLength(6)]
    public string ReferralCode { get; set; }
    
    [Description("Email message contents for sharing the deep link.")]
    public EmailMessage EmailMessage { get; set; }
    
    [Description("Sms message contents for sharing the deep link.")]
    public SmsMessage SmsMessage { get; set; }
    
    [Description("List of users referred by the current user.")]
    public IEnumerable<GetReferral> Referrals { get; set; }

    public InviteFriendsModel(string referralCode)
    {
        const string siteUrl = "https://cartoncaps.link/abfilefa90p?referral_code="; //TODO: Possibly get this from config depending on environment
        Description = "When you share your referral code ... Carton Caps love and helping our schools!";
        ReferralCode = referralCode;
        EmailMessage = new EmailMessage()
        {
            Subject = "You're invited to try the Carton Caps app!",
            Body = $"Hey! \r\n\r\n Join me in earning cash for our school ... Download the Carton Caps app here: {siteUrl}{referralCode}"
        };
        SmsMessage = new SmsMessage()
        {
            Message = $"Hi! Join me in earning cash for our school ... use the link below to download the Carton Caps app here: {siteUrl}{referralCode}"
        };
        Referrals = new List<GetReferral>();
    }
}