﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS.Base
{
    public class MODELAPIBasic
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}