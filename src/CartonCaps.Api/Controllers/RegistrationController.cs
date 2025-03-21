using CartonCaps.Api.Entities;
using CartonCaps.Api.Interfaces;
using CartonCaps.Api.Models;
using CartonCaps.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CartonCaps.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly ILogger<RegistrationController> _logger;
    private readonly IReferralService _referralService;

    public RegistrationController(ILogger<RegistrationController> logger, IReferralService referralService)
    {
        _logger = logger;
        _referralService = referralService;
    }
    
    [HttpGet]
    [EndpointName("Register")]
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
    [EndpointName("Post Registration")]
    [EndpointSummary("Create Registration")]
    [EndpointDescription("Registration endpoint to create a user.")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public IActionResult Register(PostRegistration registration)
    {
        //TODO: Check referral token, create user, and return id
        var member = new Member
        {
            FirstName = registration.FirstName,
            LastName = registration.LastName,
            // rest of member creation, for this example only first and last name are needed.
        };

        // add referral code if available
        if (!string.IsNullOrEmpty(registration.ReferralCode)){
            try
            {
                _referralService.AddReferral(member, registration.ReferralCode);
            }
            catch (Exception ex)
            {
                // log invalid member codes
                _logger.LogError(ex, ex.Message);
            }
        }
        
        return Created();
    }
}