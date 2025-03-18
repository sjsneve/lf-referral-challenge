using CartonCaps.Api.Entities;
using CartonCaps.Api.Interfaces;
using CartonCaps.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IMemberService _memberService;
    private readonly IReferralService _referralService;

    public AccountController(ILogger<AccountController> logger, IMemberService memberService, IReferralService referralService)
    {
        _logger = logger;
        _memberService = memberService;
        _referralService = referralService;
    }

    [HttpGet]
    [EndpointName("Get Invite Friends")]
    [EndpointSummary("Get Invite Friends")]
    [EndpointDescription("Retrieves data for the invite friends page, including referral code and messages.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public ActionResult<InviteFriendsModel> InviteFriends()
    {
        // get member by id so we can get their referral code
        // TODO: Remove hardcoded numbers (get from auth token)
        var member = _memberService.GetMember(1);
        var model = new InviteFriendsModel(member.ReferralCode);
        
        var referrals = _referralService.GetReferralsByReferralCode(member.ReferralCode);
        model.Referrals = referrals.Select(r => new GetReferral()
        {
            Name = r.Name,
            Status = r.Status
        });
        
        return Ok(model);
    }
}