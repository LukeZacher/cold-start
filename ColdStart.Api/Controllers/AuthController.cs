using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ColdStart.Api.Models.DTOs.Auth;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public AuthController(
        UserManager<User> userManager,
        IConfiguration configuration,
        IEmailService emailService
    )
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
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

        string confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string confirmationLink =
            $"{_configuration["ClientUrl"]}/auth/confirm-email?token={Uri.EscapeDataString(confirmationToken)}&email={Uri.EscapeDataString(user.Email!)}";

        await _emailService.SendConfirmationEmailAsync(user.Email!, confirmationLink);

        return Ok("Registration successful. Please check your email to confirm your account.");
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

        return Ok("Email confirmed successfully. You can now log in.");
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
}
