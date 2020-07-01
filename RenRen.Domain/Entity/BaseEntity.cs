using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RenRen.Domain.Entity
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(FiledLength.Guid)]
        [Column("id", Order = 0)]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time", Order = 93)]
        public virtual DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        [MaxLength(FiledLength.Guid)]
        [Column("create_user", Order = 94)]
        public virtual string CreateUserId { get; set; }

        /// <summary>
        /// 日志记录
        /// </summary>
        [MaxLength(FiledLength.Long)]
        [Column("logs", Order = 95)]
        public virtual string Logs { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [MaxLength(FiledLength.Guid)]
        [Column("tenant_id", Order = 96)]
        public virtual string TenantId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("deleted", Order = 97)]
        public virtual bool Deleted { get; set; } = false;

        /// <summary>
        /// 删除用户
        /// </summary>
        [MaxLength(FiledLength.Guid)]
        [Column("delete_user", Order = 98)]
        public virtual string DeleteUser { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("delete_time", Order = 99)]
        public virtual DateTime? DeleteTime { get; set; }
    }

    public static class FiledLength
    {
        public const int Short = 8;
        public const int Medium = 64;
        public const int Long = 1024;
        public const int Password = 512;
        public const int Guid = 32;
        public const int Name = 128;
        public const int Mobile = 16;
    }


}
