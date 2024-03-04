using Common_Layer.RequestModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Services
{
    public class UserRepository : IUserInterface
    {
        private readonly EntityContext context;
        private readonly IConfiguration config;
        private readonly ILogger<UserRepository> logger;    
        public UserRepository(EntityContext _Context,IConfiguration _config, ILogger<UserRepository> _logger)
        {
            context = _Context;
            config = _config;
            logger = _logger;
        }
        public UserEntity UserRegistration(RegisterModel model)
        {
            UserEntity userEntity = new UserEntity();
            if (!CheckEmail(model.email))
            {
                logger.LogInformation("Registration Successful");
                userEntity.firstname = model.firstname;
                userEntity.lastname = model.lastname;
                userEntity.email = model.email;
                userEntity.password = Encrypt(model.password);
            }
            else
                throw new Exception("User Already Exists ...");
            context.UserTable.Add(userEntity);
            context.SaveChanges();
            return userEntity;
        }
        public async Task<string> LoginUser(LoginModel model)
        {
            var entity = await context.UserTable.SingleOrDefaultAsync(user => user.email == model.email);
            if (entity == null)
            {
                throw new ArgumentException("User Does not exist ...");
            }
            else
            {
                if(Decrypt(model.password, entity.password))
                {
                    string token = GenerateToken(entity.email, entity.UserId);
                    return token;
                }
                    throw new ArgumentException("Password is incorrect ...");
            }
        }
        public bool CheckEmail(string email)
        {
            var user = context.UserTable.SingleOrDefault(user => user.email == email);
            return user != null;
        }

        public string Encrypt(string password)
        {
            int sal_Len = new Random().Next(10, 13);
            string salt = BCrypt.Net.BCrypt.GenerateSalt(sal_Len);
            string hashing_pass = BCrypt.Net.BCrypt.HashPassword(password,salt);
            return hashing_pass;
        }
        public bool Decrypt(string password,string password1) 
        {
            return BCrypt.Net.BCrypt.Verify(password,password1);
        }
        private string GenerateToken(string Email, int Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
             new Claim("Email",Email),
             new Claim("User Id",Id.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public ForgetPasswordModel ForgetPassword(string email)
        {
            var entity = context.UserTable.SingleOrDefault(user => user.email == email);
            ForgetPasswordModel model = new ForgetPasswordModel();
            model.UserId = entity.UserId;
            model.email = entity.email;
            model.token = GenerateToken(email, entity.UserId);
            return model;
        }
        public string ResetPassword(string email,ResetPasswordModel model)
        {
            if(model.new_password == model.confirm_password)
            {
                if (CheckEmail(email))
                {
                    var entity = context.UserTable.SingleOrDefault(user => user.email == email);
                    entity.password = Encrypt(model.new_password);
                    context.SaveChanges();
                    return "true";
                }
                throw new Exception("Such Email does not exist...");
            }
            throw new Exception("Password Does not match...");
        }
    }
}
 