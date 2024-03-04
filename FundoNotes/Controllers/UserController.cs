using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Common_Layer.Utility;
using Manager_Layer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IBus bus;
        public UserController(IUserManager usermanager, IBus bus)
        {
            _usermanager = usermanager;
            this.bus = bus;

        }
        [HttpPost]
        [Route("Reg")]
        public ActionResult Registration(RegisterModel model)
        {
            
            try
            {
                    return Ok(new ResponseModel<UserEntity> {
                                                    Success = true,
                                                    Message = "Registration Successful",
                                                    data = _usermanager.UserRegistration(model)
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<UserEntity>
                {
                    Success = false,
                    Message = ex.Message,
                    data = null
                });
            }
        }
        [HttpPost]
        [Route("log")]
        public async Task<ActionResult> Login(LoginModel model)
        {
             string token= await _usermanager.LoginUser(model);
            try
            {
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Login Successful",
                    data = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    data = token
                });
            }
        }
        [HttpPost]
        [Route("forget")]
        public async Task<ActionResult> ForgetPassword(string Email)
        {
            try
            {
                if (_usermanager.CheckEmail(Email))
                {
                    SendMail mail = new SendMail();
                    ForgetPasswordModel model = _usermanager.ForgetPassword(Email);
                    string str = mail.Send_Mail(model.email, model.token);
                    Uri uri = new Uri("rabbitmq://localhost/FunfooNotesEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);
                    await endPoint.Send(model);
                    return Ok(new ResponseModel<string> { Success = true, Message = str, data = model.token });
                }
                else
                {
                    throw new Exception("Failed to send email");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = ex.Message, data = null });
            }
        }
        [Authorize]
        [HttpPost]
        [Route("reset")]
        public ActionResult Reset(ResetPasswordModel model)
        {
            try
            {
                string email = User.FindFirst("Email").Value;
                return Ok(new ResponseModel<string>
                {
                    Success = true,
                    Message = "Password Reset Successful",
                    data = _usermanager.ResetPassword(email, model)
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    data = "Password reset unsuccessful"
                });
            }
        }
    }
}
