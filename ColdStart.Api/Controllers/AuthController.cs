using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ColdStart.Api.Models.DTOs.Auth;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Options;
using ColdStart.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ColdStart.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly ILogger<AuthController> _logger;
    private readonly IOptions<AuthOptions> _authOptions;

    public AuthController(
        UserManager<User> userManager,
        IConfiguration configuration,
        IEmailService emailService,
        ILogger<AuthController> logger,
        IOptions<AuthOptions> authOptions
    )
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
        _logger = logger;
        _authOptions = authOptions;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        User user = new User { Email = registerRequest.Email, UserName = registerRequest.Email };

        IdentityResult identityResult = await _userManager.CreateAsync(
            user,
            registerRequest.Password
        );

        if (!identityResult.Succeeded)
        {
            return BadRequest(identityResult.Errors);
        }

        string confirmationLink = await GenerateEmailConfirmationLinkAsync(user);

        try
        {
            await _emailService.SendConfirmationEmailAsync(user.Email!, confirmationLink);
        }
        catch (Exception ex)
        {
            await _userManager.DeleteAsync(user);
            _logger.LogError(ex, "Failed to send confirmation email to {Email}", user.Email);
            return StatusCode(
                500,
                "Registration succeeded but we couldn't send the confirmation email. Please contact support at support@coldstart.fun"
            );
        }

        return Ok(
            new
            {
                message = "Registration successful. Please check your email to confirm your account.",
            }
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        User? user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null || !(await _userManager.CheckPasswordAsync(user, loginRequest.Password)))
        {
            return Unauthorized("Invalid email or password.");
        }

        if (!user.EmailConfirmed)
        {
            return Unauthorized("Please confirm your email before logging in.");
        }

        string token = GenerateToken(user);

        return Ok(new AuthResponse { Token = token, Email = user.Email! });
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] string token,
        [FromQuery] string email
    )
    {
        User? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return BadRequest("Invalid email confirmation request.");
        }

        IdentityResult identityResult = await _userManager.ConfirmEmailAsync(user, token);

        if (!identityResult.Succeeded)
        {
            return BadRequest("Invalid or expired confirmation token.");
        }

        return Ok(new { message = "Email confirmed successfully. You can now log in." });
    }

    [HttpPost("resend-email-confirmation")]
    public async Task<IActionResult> ResendEmailConfirmation(
        [FromBody] ResendEmailConfirmationRequest resendEmailConfirmationRequest
    )
    {
        User? user = await _userManager.FindByEmailAsync(resendEmailConfirmationRequest.Email);

        if (user != null && !user.EmailConfirmed)
        {
            IdentityResult newSecurityStamp = await _userManager.UpdateSecurityStampAsync(user);
            if (!newSecurityStamp.Succeeded)
            {
                _logger.LogWarning(
                    "Failed to update security stamp for {Email} while resending account confirmation mail.",
                    user.Email
                );
            }

            string confirmationLink = await GenerateEmailConfirmationLinkAsync(user);

            try
            {
                await _emailService.SendConfirmationEmailAsync(user.Email!, confirmationLink);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to resend account confirmation mail to {Email}.",
                    user.Email
                );
            }
        }

        return Ok(
            new ResendEmailConfirmationResponse
            {
                CooldownSeconds = _authOptions.Value.ResendAccountConfirmationMailCooldownSeconds,
            }
        );
    }

    private string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        ];

        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> GenerateEmailConfirmationLinkAsync(User user)
    {
        string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string confirmationLink =
            $"{_configuration["ClientUrl"]}/auth/confirm-email?token={Uri.EscapeDataString(confirmationToken)}&email={Uri.EscapeDataString(user.Email!)}";
        return confirmationLink;
    }
}
