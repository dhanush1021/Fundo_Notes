using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common_Layer.RequestModel
{
    public class ForgetPasswordModel
    {
        public int UserId { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }
}
