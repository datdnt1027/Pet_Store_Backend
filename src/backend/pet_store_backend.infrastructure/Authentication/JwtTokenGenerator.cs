﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Services;
using pet_store_backend.domain.Entities.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using pet_store_backend.infrastructure.Persistence.Common;


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
        public string GenerateToken(UserRole user, List<UserPermission> permissions)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.User.Id.Value.ToString()),
                new(JwtRegisteredClaimNames.GivenName, user.User.FirstName),
                new(JwtRegisteredClaimNames.FamilyName, user.User.LastName),
                new(JwtRegisteredClaimNames.Jti, user.Id.Value.ToString()),
                new(ClaimTypes.Role, user.UserRoleName)
            };

            if (user.UserRoleName != UserRoleKey.UserRoleName)
            {
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
            }

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
