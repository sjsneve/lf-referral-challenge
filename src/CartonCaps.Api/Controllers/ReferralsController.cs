using System.Security.Claims;
using CartonCaps.Api.Entities;
using CartonCaps.Api.Interfaces;
using CartonCaps.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReferralsController : ControllerBase
{
    private readonly ILogger<ReferralsController> _logger;
    private readonly IMemberService _memberService;
    private readonly IReferralService _referralService;

    public ReferralsController(ILogger<ReferralsController> logger, IMemberService memberService, IReferralService referralService)
    {
        _logger = logger;
        _memberService = memberService;
        _referralService = referralService;
    }

    [HttpGet("invitefriends")]
    [Authorize(Roles = "User")]
    [EndpointName("GetInviteFriends")]
    [EndpointSummary("Get Invite Friends")]
    [EndpointDescription("Retrieves data for the invite friends page, including referral code and messages.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<InviteFriendsModel>> InviteFriends()
    {
        // get member by id so we can get their referral code
        var memberIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(memberIdString, out var memberId))
        {
            return BadRequest("Member ID is invalid.");
        }

        var member = _memberService.GetMember(memberId);
        var model = new InviteFriendsModel(member.ReferralCode);
        
        var referrals = await _referralService.GetReferralsByReferralCode(member.ReferralCode);
        model.Referrals = referrals.Select(r => new ReferralDTO()
        {
            Name = r.ReferredMember.FirstName + " " + r.ReferredMember.LastName,
            Status = r.Status
        });
        
        return Ok(model);
    }

    [HttpGet("registration")]
    [EndpointName("GetRegistration")]
    [EndpointSummary("Get Registration")]
    [EndpointDescription("Registration endpoint w/ referral code to attach to referrer.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<GetRegistration> Register([FromQuery(Name = "referral_code")] string? referralCode)
    {
        // prepopulate referral code if exists
        var model = new GetRegistration(referralCode);
        return Ok(model);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    [EndpointName("CreateReferral")]
    [EndpointSummary("Create Referral")]
    [EndpointDescription("Create a referral for user.")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult CreateReferral(CreateReferralDTO model)
    {
        // get member by id so we can get their referral code
        Member? member = null;
        var memberIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(memberIdString, out var memberId))
        {
            member = _memberService.GetMember(memberId);
        }

        if (member == null)
        {
            return BadRequest("Member ID is invalid.");
        }

        if (model.ReferralCode == null)
        {
            return BadRequest("Referral code is required.");
        }

        // add referral code if available
        var referral = _referralService.AddReferral(member.Id, model.ReferralCode);

        if (referral == null)
        {
            return BadRequest("Invalid referral code.");
        }
        
        return Created();
    }
}