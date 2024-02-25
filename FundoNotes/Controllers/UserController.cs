using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;
using System;
using System.Threading.Tasks;

namespace FundoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _usermanager;
        public UserController(IUserManager usermanager)
        {
            _usermanager = usermanager;
        }
        [HttpPost]
        [Route("Reg")]
        public ActionResult Registration(RegisterModel model)
        {
            var status = _usermanager.UserRegistration(model);
            if(status != null)
            {
                return Ok(new ResponseModel<UserEntity> {
                                                Success = true,
                                                Message = "Registration Successful",
                                                data = status
                                            });
            }
            return BadRequest(new ResponseModel<UserEntity>
            {
                Success = false,
                Message = "Registration Failed",
                data = status
            });
        }
        [HttpPost]
        [Route("log")]
        public async Task<ActionResult> Login(LoginModel model)
        {
             var entity= await _usermanager.LoginUser(model);
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("User Does Not Exist !!");
                }
                else
                {
                    if(entity.password == model.password)
                    {
                        return Ok(new ResponseModel<UserEntity>
                        {
                            Success = true,
                            Message = "Login Successful",
                            data = entity
                        });
                    }
                    else
                    {
                        throw new ArgumentException("Password Incorrect !!");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<UserEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = entity
                });
            }
            
        }
    }
}
