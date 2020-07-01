using System;
using System.Collections.Generic;
using System.Net;

namespace RenRen.Domain.Common.Utils
{
    /// <summary>
    /// 接口返回对象
    /// </summary>
    public class R : Dictionary<string, object>
    {
        public R(int code = 0, string msg = "success")
        {
            this.Add("code", code);
            this.Add("msg", msg);
        }

        public static R Error(string msg = "未知异常，请联系管理员")
        {
            return Error((int)HttpStatusCode.InternalServerError, msg);
        }

        public static R Error(int code, string msg)
        {
            return new R(code, msg);
        }

        public static R Ok(string msg)
        {
            return new R(msg: msg);
        }

        public static R Ok(Dictionary<string, object> map)
        {
            R r = new R();
            foreach (var item in map)
            {
                r.TryAdd(item.Key, item.Value);
            }
            return r;
        }

        public static R Ok()
        {
            return new R();
        }

        public R Put(string key, object value)
        {
            this.Add(key, value);
            return this;
        }

        public R Put(object value)
        {
            this.TryAdd("data", value);
            return this;
        }
    }
}
