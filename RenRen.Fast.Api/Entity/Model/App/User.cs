using RenRen.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RenRen.Fast.Api.Entity
{
    /// <summary>
    /// APP用户
    /// </summary>
    [Table("app_user")]
    public partial class User : BaseEntity
    {
        /// <summary>
        /// 邮件
        /// </summary>
        [Column("mail")]
        [MaxLength(FiledLength.Medium)]
        public string Mail { get; set; }
        [Column("user_name")]
        [MaxLength(FiledLength.Name)]
        public string UserName { get; set; }
        [Column("mobile")]
        [MaxLength(FiledLength.Mobile)]
        public string Mobile { get; set; }
        [Column("password")]
        [MaxLength(FiledLength.Password)]
        public string Password { get; set; }

        [JsonIgnore]
        [Column("salt")]
        [MaxLength(FiledLength.Guid)]
        public string Salt { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        [Column("status")]
        public UserStatus Status { get; set; } = UserStatus.正常;

        #region
        [JsonIgnore]
        [Column("create_time")]
        public override DateTime CreateTime { get => base.CreateTime; set => base.CreateTime = value; }
        [JsonIgnore]
        [Column("create_user_id")]
        [MaxLength(FiledLength.Guid)]
        public override string CreateUserId { get => base.CreateUserId; set => base.CreateUserId = value; }
        [JsonIgnore]
        [Column("deleted")]
        public override bool Deleted { get => base.Deleted; set => base.Deleted = value; }
        [JsonIgnore]
        [Column("delete_time")]
        public override DateTime? DeleteTime { get => base.DeleteTime; set => base.DeleteTime = value; }
        [JsonIgnore]
        [Column("delete_user")]
        [MaxLength(FiledLength.Guid)]
        public override string DeleteUser { get => base.DeleteUser; set => base.DeleteUser = value; }
        [Column("logs")]
        [JsonIgnore]
        [MaxLength(FiledLength.Long)]
        public override string Logs { get => base.Logs; set => base.Logs = value; }
        [NotMapped]
        public override string TenantId { get => base.TenantId; set => base.TenantId = value; }
        #endregion
    }

    public enum UserStatus
    {
        正常,
        禁用
    }
}
