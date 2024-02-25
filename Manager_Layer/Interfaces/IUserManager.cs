using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Layer.Interfaces
{
    public interface IUserManager
    {
        UserEntity UserRegistration(RegisterModel model);
        public Task<UserEntity> LoginUser(LoginModel model);
    }
}
