using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using RenRen.Fast.Api.Entity.Msg;

namespace RenRen.Fast.Api.Modules.Msg.Form
{
    public class SmsDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required]
        public string Mobile { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Required]
        public MsgType MsgType { get; set; }
    }
}
