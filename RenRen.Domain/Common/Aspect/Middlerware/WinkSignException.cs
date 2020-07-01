using System;
using System.Collections.Generic;
using System.Text;

namespace RenRen.Domain.Common.Aspect.Middlerware
{
    public class WinkSignException : System.Exception
    {
        public String Msg;
        public int Code = 501;

        public WinkSignException(String msg) : base(msg)
        {
            this.Msg = msg;
        }

        public WinkSignException(String msg, int code) : base(msg)
        {
            this.Msg = msg;
            this.Code = code;
        }
    }
}
