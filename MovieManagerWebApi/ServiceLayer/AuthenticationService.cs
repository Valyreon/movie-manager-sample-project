using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using DataLayer.Interfaces;
using Domain;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace ServiceLayer
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork uow;

        public AuthenticationService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public LoginResponse Login(LoginRequest request)
        {
            var user = uow.Users.GetUserByEmail(request.Email);

            if (user == null)
            {
                return LoginResponse.InvalidCreds;
            }

            var validCreds = user.IsPasswordValid(request.Password);

            if (!validCreds)
            {
                return LoginResponse.InvalidCreds;
            }

            return new LoginResponse
            {
                Success = true,
                Message = "Successful login.",
                Token = GenerateAccessToken(user.Email, user.Username)
            };
        }


        public SignupResponse Signup(SignupRequest request)
        {
            // check password strength
            if (!IsPasswordStrongEnough(request.Password))
            {
                return new SignupResponse()
                {
                    Success = false,
                    Message = "Your password is too weak. Password must have one uppercase, one lowercase letter, a number and must be 6 or more characters long."
                };
            }

            // check if email is already used
            if (uow.Users.GetUserByEmail(request.Email) != null)
            {
                return new SignupResponse()
                {
                    Success = false,
                    Message = "Email is already taken."
                };
            }

            if (uow.Users.GetUserByUsername(request.Username) != null)
            {
                return new SignupResponse()
                {
                    Success = false,
                    Message = "Username is already taken."
                };
            }

            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                About = string.IsNullOrWhiteSpace(request.About) ? null : request.About,
                IsPrivate = request.IsPrivate
            };

            newUser.SetPassword(request.Password);
            uow.Users.Add(newUser);
            uow.Commit();

            return new SignupResponse()
            {
                Success = true,
                Message = "Successful signup."
            };
        }

        private bool IsPasswordStrongEnough(string password)
        {
            // ensure password has at least 6 characters, one uppercase, one lowercase letter and a number
            return Regex.IsMatch(password, "^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{6,}$");
        }

        private string GenerateAccessToken(string email, string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email)
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: DateTime.Now.AddDays(90),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
