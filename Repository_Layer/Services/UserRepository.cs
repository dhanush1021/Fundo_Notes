using Common_Layer.RequestModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Services
{
    public class UserRepository : IUserInterface
    {
        private readonly EntityContext context;
        public UserRepository(EntityContext _Context)
        {
            context = _Context;
        }
        public UserEntity UserRegistration(RegisterModel model)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.firstname = model.firstname;
            userEntity.lastname = model.lastname;
            userEntity.email = model.email;
            userEntity.password = Encrypt(model.password);
            context.UserTable.Add(userEntity);
            context.SaveChanges();
            return userEntity;
        }
        public async Task<UserEntity> LoginUser(LoginModel model)
        {
            var entity = await context.UserTable.SingleOrDefaultAsync(user => user.email == model.email);
            if (entity == null)
            {
                return null;
            }
            else
            {
                if(Decrypt(model.password, entity.password))
                {
                    return entity;
                }
                else
                {
                    entity.password = "incorrect";
                    return entity;
                }
            }
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
    }
}
