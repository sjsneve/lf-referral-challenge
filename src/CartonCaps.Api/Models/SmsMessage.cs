using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CartonCaps.Api.Models;

public class SmsMessage
{
    [Description("The sms message body")]
    [MaxLength(500)]
    public string Message { get; set; } = string.Empty;
}