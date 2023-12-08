using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using pet_store_backend.domain.Entities.Users;


namespace pet_store_backend.infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSetting _jwtSettings;
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSetting> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
        }
        public string GenerateTokenCustomer(UserRole customer)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, customer.Customer.Id.Value.ToString()),
                new(JwtRegisteredClaimNames.GivenName, customer.Customer.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, customer.Customer.LastName),
                new(JwtRegisteredClaimNames.Jti, customer.Id.Value.ToString()),
                new(ClaimTypes.Role, customer.UserRoleName)
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }

        public string GenerateTokenUser(UserRole userRole, List<UserPermission> permissions)
        {
            var signingCredentials = new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
               SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userRole.User.Id.Value.ToString()),
                new(JwtRegisteredClaimNames.GivenName, userRole.User.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, userRole.User.LastName),
                new(JwtRegisteredClaimNames.Jti, userRole.Id.Value.ToString()),
                new(ClaimTypes.Role, userRole.UserRoleName)
            };

            var tablePermissions = new Dictionary<string, TablePermission>();

            foreach (var permission in permissions)
            {
                // Add the table permission to the dictionary
                tablePermissions[permission.TableName] = new TablePermission
                {
                    Create = permission.Create,
                    Read = permission.Read,
                    Update = permission.Update,
                    Deactivate = permission.Deactive
                };
            }

            // Add a single claim for all table permissions in key-value format
            claims.Add(new Claim("permissions", JsonConvert.SerializeObject(tablePermissions)));

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
