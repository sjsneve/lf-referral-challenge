using CartonCaps.Api.Entities;

namespace CartonCaps.Api.Interfaces;

public interface IMemberService
{
    Member GetMember(int memberId);
}