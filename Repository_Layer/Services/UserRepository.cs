using Common_Layer.RequestModel;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
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
            userEntity.password = model.password;
            context.UserTable.Add(userEntity);
            context.SaveChanges();
            return userEntity;
        }
        public  async Task<UserEntity> LoginUser(LoginModel model)
        {
            return await context.UserTable.SingleOrDefaultAsync(user => user.email == model.email);
        }
    }
}
