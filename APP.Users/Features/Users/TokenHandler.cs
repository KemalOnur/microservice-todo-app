﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Users
{
    public class TokenRequest : Request, IRequest<TokenResponse>
    {
        [Required, StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required, StringLength(10, MinimumLength = 3)]
        public string Password { get; set; }

        [JsonIgnore]
        public override int Id { get => base.Id; set => base.Id = value; }
    }

    public class TokenResponse : CommandResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public TokenResponse(bool isSuccessful, string message = "", int id = 0) : base(isSuccessful, message, id)
        {
        }
    }

    public class TokenHandler : UsersDbHandler, IRequestHandler<TokenRequest, TokenResponse>
    {
        public TokenHandler(UsersDb db) : base(db)
        {
        }

        public async Task<TokenResponse> Handle(TokenRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.UserName == request.UserName && u.Password == request.Password && u.IsActive);

            if (user is null)
                return new TokenResponse(false, "User Not Found");

            //refresh token
            user.RefreshToken = CreateRefreshToken();
            user.RefreshTokenExpiration = DateTime.Now.AddDays(14);
            _db.Users.Update(user);
            await _db.SaveChangesAsync(cancellationToken);

            var claims = GetClaims(user);
            var expiration = DateTime.Now.AddMinutes(AppSettings.ExpirationInMinutes);
            var token = CreateAccessToken(claims, expiration);
            return new TokenResponse(true, "Token Established", user.Id)
            {
                Token = JwtBearerDefaults.AuthenticationScheme + " " + token,
                RefreshToken = user.RefreshToken
            };

        }
    }
    
}
