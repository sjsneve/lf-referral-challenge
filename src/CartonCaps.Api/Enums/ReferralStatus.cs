using System.Text.Json.Serialization;

namespace CartonCaps.Api.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<ReferralStatus>))]
public enum ReferralStatus
{
    Complete
}