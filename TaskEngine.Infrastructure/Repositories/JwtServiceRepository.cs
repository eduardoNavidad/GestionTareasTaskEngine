using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;
using TaskEngine.Infrastructure.Persistence;


namespace TaskEngine.Infrastructure.Repositories;

public class JwtServiceRepository : IJwtServiceRepository
{
    private readonly AppDbContext _dbcontext;
    private readonly IConfiguration _configuration;
    private readonly IPasswordService _passwordService;

    public JwtServiceRepository(AppDbContext context, IConfiguration configuration, IPasswordService passwordService)
    {
        _dbcontext = context;
        _configuration = configuration;
        _passwordService = passwordService;
    }
    public async Task<LoginResponseModel?> Authenticate(LoginRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            return null;

        var userAccount = await _dbcontext.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
        if (userAccount is null || !_passwordService.VerifyPassword(request.Password, userAccount.Password!))
            return null;

        var issuer = _configuration["JwtConfig:Issuer"];
        var audience = _configuration["JwtConfig:Audience"];
        var Key = _configuration["JwtConfig:Key"];
        //var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
        var tokenValidityMins = int.Parse(_configuration["JwtConfig:TokenValidityMins"] ?? "30");
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(JwtRegisteredClaimNames.Name,request.UserName)
                }),
            Expires = tokenExpiryTimeStamp,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key!)),
                                                        SecurityAlgorithms.HmacSha512Signature),

        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new LoginResponseModel
        {
            AccessToken = accessToken,
            UserName = request.UserName,
            ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
        };
    }
}
