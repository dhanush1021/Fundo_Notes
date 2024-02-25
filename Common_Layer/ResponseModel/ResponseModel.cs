﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.ResponseModel
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T data { get; set; }
    }
}
