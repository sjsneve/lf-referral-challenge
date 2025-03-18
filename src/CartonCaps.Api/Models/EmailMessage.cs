using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CartonCaps.Api.Models;

public class EmailMessage
{
    [Description("The email subject")]
    [MaxLength(50)]
    public string Subject { get; set; } = string.Empty;
    
    [Description("The email body")]
    [MaxLength(500)]
    public string Body { get; set; } = string.Empty;
}