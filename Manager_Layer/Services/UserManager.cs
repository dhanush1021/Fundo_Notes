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
        public Task<string> LoginUser(LoginModel model)
        {
            return userInterface.LoginUser(model);
        }
        public ForgetPasswordModel ForgetPassword(string email)
        {
            return userInterface.ForgetPassword(email);
        }
        public bool CheckEmail(string email)
        {
            return userInterface.CheckEmail(email);
        }
        public string ResetPassword(string email, ResetPasswordModel model)
        {
            return userInterface.ResetPassword(email, model);
        }
    }
}
