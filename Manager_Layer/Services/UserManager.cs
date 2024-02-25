using Common_Layer.RequestModel;
using Manager_Layer.Interfaces;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Layer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserInterface userInterface;
        public UserManager(IUserInterface _userInterface)
        {
            userInterface = _userInterface;
        }
        public UserEntity UserRegistration(RegisterModel model)
        {
            return userInterface.UserRegistration(model);
        }
        public Task<UserEntity> LoginUser(LoginModel model)
        {
            return userInterface.LoginUser(model);
        }
    }
}
