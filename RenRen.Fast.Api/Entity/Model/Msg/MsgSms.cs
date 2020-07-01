using Newtonsoft.Json;
using RenRen.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenRen.Fast.Api.Entity.Msg
{
    [Table("msg_sms")]
    public class MsgSms
    {
        public MsgSms() { }

        public MsgSms(string content, DateTime expiredTime, string mobile, MsgType msgType = default)
        {
            Content = content;
            ExpiredTime = expiredTime;
            Mobile = mobile;
            MsgType = msgType;
        }


        /// <summary>
        /// id
        /// </summary>
        [Key]
        [MaxLength(FiledLength.Guid)]
        [Column("id")]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 短信内容
        /// </summary>
        [MaxLength(80)]
        [Column("content")]
        [Required]
        public virtual string Content { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [Column("send_time")]
        public virtual DateTime SendTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 过期时间
        /// </summary>
        [Column("expired_time")]
        public virtual DateTime ExpiredTime { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(FiledLength.Mobile)]
        [Column("mobile")]
        [Required]
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 信息类型
        /// </summary>
        [Column("msg_type")]
        public virtual MsgType MsgType { get; set; }

        #region
        [Column("create_time")]
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;
        [Column("create_user_id")]
        [MaxLength(FiledLength.Guid)]
        public virtual string CreateUserId { get; set; }
        [Column("deleted")]
        public virtual bool Deleted { get; set; } = false;
        #endregion
    }
}
