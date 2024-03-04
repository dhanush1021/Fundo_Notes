using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.Interfaces
{
    public interface IUserInterface
    {
        public UserEntity UserRegistration(RegisterModel model);
        public Task<string> LoginUser(LoginModel model);
        public ForgetPasswordModel ForgetPassword(string email);
        public bool CheckEmail(string email);
        public string ResetPassword(string email, ResetPasswordModel model);
    }
}
