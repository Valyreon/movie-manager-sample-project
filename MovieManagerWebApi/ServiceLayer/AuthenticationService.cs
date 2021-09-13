using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using DataLayer.Interfaces;
using Domain;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Exceptions;
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

            // if user with that email does not exist
            if (user == null)
            {
                throw new InvalidLoginCredentialsException("No user matches those credentials.");
            }

            // check password
            var validCreds = user.IsPasswordValid(request.Password);

            // if password is not valid
            if (!validCreds)
            {
                throw new InvalidLoginCredentialsException("No user matches those credentials.");
            }

            // everything is okay, generate token for user
            return new LoginResponse
            {
                Token = GenerateAccessToken(user, request.RememberMe)
            };
        }


        public void Signup(SignupRequest request)
        {
            // check password strength
            if (!IsPasswordStrongEnough(request.Password))
            {
                throw new PasswordTooWeakException("Your password is too weak. Password must have one uppercase, " +
                    "one lowercase letter, a number and must be 6 or more characters long.");
            }

            // check if email is already used
            if (uow.Users.GetUserByEmail(request.Email) != null)
            {
                throw new UserAlreadyExistsException("Email is already taken.");
            }

            // check if username already exists
            if (uow.Users.GetUserByUsername(request.Username) != null)
            {
                throw new UserAlreadyExistsException("Username is already taken.");
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
        }

        private bool IsPasswordStrongEnough(string password)
        {
            // ensure password has at least 6 characters, one uppercase, one lowercase letter and a number
            return Regex.IsMatch(password, "^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{6,}$");
        }

        private string GenerateAccessToken(User user, bool rememberMe = false)
        {
            var keyString = Environment.GetEnvironmentVariable("MovieManagerJwtKey");

            if(keyString == null)
            {
                throw new KeyNotFoundException("Jwt key environment variable not found.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString ?? "secret"));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("About", user.About),
                new Claim("IsPrivate", user.IsPrivate.ToString())
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: rememberMe ? DateTime.Now.AddDays(90) : (DateTime?)null,
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
