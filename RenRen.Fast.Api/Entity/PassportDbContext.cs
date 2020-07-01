using System;
using Microsoft.EntityFrameworkCore;
using RenRen.Fast.Api.Entity.Msg;

namespace RenRen.Fast.Api.Entity
{
    public partial class PassportDbContext : DbContext
    {
        public PassportDbContext()
        {
        }

        public PassportDbContext(DbContextOptions<PassportDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<MsgSms> MsgSms { get; set; }

        public virtual DbSet<QrtzBlobTriggers> QrtzBlobTriggers { get; set; }
        public virtual DbSet<QrtzCalendars> QrtzCalendars { get; set; }
        public virtual DbSet<QrtzCronTriggers> QrtzCronTriggers { get; set; }
        public virtual DbSet<QrtzFiredTriggers> QrtzFiredTriggers { get; set; }
        public virtual DbSet<QrtzJobDetails> QrtzJobDetails { get; set; }
        public virtual DbSet<QrtzLocks> QrtzLocks { get; set; }
        public virtual DbSet<QrtzPausedTriggerGrps> QrtzPausedTriggerGrps { get; set; }
        public virtual DbSet<QrtzSchedulerState> QrtzSchedulerState { get; set; }
        public virtual DbSet<QrtzSimpleTriggers> QrtzSimpleTriggers { get; set; }
        public virtual DbSet<QrtzSimpropTriggers> QrtzSimpropTriggers { get; set; }
        public virtual DbSet<QrtzTriggers> QrtzTriggers { get; set; }
        public virtual DbSet<ScheduleJob> ScheduleJob { get; set; }
        public virtual DbSet<ScheduleJobLog> ScheduleJobLog { get; set; }
        public virtual DbSet<SysCaptcha> SysCaptcha { get; set; }
        public virtual DbSet<SysConfig> SysConfig { get; set; }
        public virtual DbSet<SysLog> SysLog { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysOss> SysOss { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRoleMenu> SysRoleMenu { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysUserRole> SysUserRole { get; set; }
        public virtual DbSet<SysUserToken> SysUserToken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Mail).HasComment("邮箱");
                entity.Property(e => e.Mobile).HasComment("手机,登陆帐号");
                entity.Property(e => e.UserName).HasComment("显示用户名");
            });

            modelBuilder.Entity<MsgSms>(entity =>
            {
                entity.Property(e => e.MsgType).HasComment("短信类型：注册 = 0");
                entity.Property(e => e.Content).HasComment("短信内容");
                entity.Property(e => e.ExpiredTime).HasComment("过期时间");
            });

            modelBuilder.Entity<QrtzBlobTriggers>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("QRTZ_BLOB_TRIGGERS");

                entity.Property(e => e.BlobData)
                    .HasColumnName("BLOB_DATA")
                    .HasColumnType("image");

                entity.Property(e => e.SchedName)
                    .IsRequired()
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerGroup)
                    .IsRequired()
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.TriggerName)
                    .IsRequired()
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<QrtzCalendars>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.CalendarName });

                entity.ToTable("QRTZ_CALENDARS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.CalendarName)
                    .HasColumnName("CALENDAR_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Calendar)
                    .IsRequired()
                    .HasColumnName("CALENDAR")
                    .HasColumnType("image");
            });

            modelBuilder.Entity<QrtzCronTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_CRON_TRIGGERS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.CronExpression)
                    .IsRequired()
                    .HasColumnName("CRON_EXPRESSION")
                    .HasMaxLength(120);

                entity.Property(e => e.TimeZoneId)
                    .HasColumnName("TIME_ZONE_ID")
                    .HasMaxLength(80);

                entity.HasOne(d => d.QrtzTriggers)
                    .WithOne(p => p.QrtzCronTriggers)
                    .HasForeignKey<QrtzCronTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                    .HasConstraintName("FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS");
            });

            modelBuilder.Entity<QrtzFiredTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.EntryId });

                entity.ToTable("QRTZ_FIRED_TRIGGERS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.EntryId)
                    .HasColumnName("ENTRY_ID")
                    .HasMaxLength(95);

                entity.Property(e => e.FiredTime).HasColumnName("FIRED_TIME");

                entity.Property(e => e.InstanceName)
                    .IsRequired()
                    .HasColumnName("INSTANCE_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.IsNonconcurrent)
                    .HasColumnName("IS_NONCONCURRENT")
                    .HasMaxLength(1);

                entity.Property(e => e.JobGroup)
                    .HasColumnName("JOB_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.JobName)
                    .HasColumnName("JOB_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.RequestsRecovery)
                    .HasColumnName("REQUESTS_RECOVERY")
                    .HasMaxLength(1);

                entity.Property(e => e.SchedTime).HasColumnName("SCHED_TIME");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("STATE")
                    .HasMaxLength(16);

                entity.Property(e => e.TriggerGroup)
                    .IsRequired()
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.TriggerName)
                    .IsRequired()
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<QrtzJobDetails>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.JobName, e.JobGroup });

                entity.ToTable("QRTZ_JOB_DETAILS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.JobName)
                    .HasColumnName("JOB_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.JobGroup)
                    .HasColumnName("JOB_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(250);

                entity.Property(e => e.IsDurable)
                    .IsRequired()
                    .HasColumnName("IS_DURABLE")
                    .HasMaxLength(1);

                entity.Property(e => e.IsNonconcurrent)
                    .IsRequired()
                    .HasColumnName("IS_NONCONCURRENT")
                    .HasMaxLength(1);

                entity.Property(e => e.IsUpdateData)
                    .IsRequired()
                    .HasColumnName("IS_UPDATE_DATA")
                    .HasMaxLength(1);

                entity.Property(e => e.JobClassName)
                    .IsRequired()
                    .HasColumnName("JOB_CLASS_NAME")
                    .HasMaxLength(250);

                entity.Property(e => e.JobData)
                    .HasColumnName("JOB_DATA")
                    .HasColumnType("image");

                entity.Property(e => e.RequestsRecovery)
                    .IsRequired()
                    .HasColumnName("REQUESTS_RECOVERY")
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<QrtzLocks>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.LockName });

                entity.ToTable("QRTZ_LOCKS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.LockName)
                    .HasColumnName("LOCK_NAME")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<QrtzPausedTriggerGrps>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerGroup });

                entity.ToTable("QRTZ_PAUSED_TRIGGER_GRPS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<QrtzSchedulerState>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.InstanceName });

                entity.ToTable("QRTZ_SCHEDULER_STATE");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.InstanceName)
                    .HasColumnName("INSTANCE_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.CheckinInterval).HasColumnName("CHECKIN_INTERVAL");

                entity.Property(e => e.LastCheckinTime).HasColumnName("LAST_CHECKIN_TIME");
            });

            modelBuilder.Entity<QrtzSimpleTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_SIMPLE_TRIGGERS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.RepeatCount).HasColumnName("REPEAT_COUNT");

                entity.Property(e => e.RepeatInterval).HasColumnName("REPEAT_INTERVAL");

                entity.Property(e => e.TimesTriggered).HasColumnName("TIMES_TRIGGERED");

                entity.HasOne(d => d.QrtzTriggers)
                    .WithOne(p => p.QrtzSimpleTriggers)
                    .HasForeignKey<QrtzSimpleTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                    .HasConstraintName("FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS");
            });

            modelBuilder.Entity<QrtzSimpropTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_SIMPROP_TRIGGERS");

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.BoolProp1)
                    .HasColumnName("BOOL_PROP_1")
                    .HasMaxLength(1);

                entity.Property(e => e.BoolProp2)
                    .HasColumnName("BOOL_PROP_2")
                    .HasMaxLength(1);

                entity.Property(e => e.DecProp1)
                    .HasColumnName("DEC_PROP_1")
                    .HasColumnType("numeric(13, 4)");

                entity.Property(e => e.DecProp2)
                    .HasColumnName("DEC_PROP_2")
                    .HasColumnType("numeric(13, 4)");

                entity.Property(e => e.IntProp1).HasColumnName("INT_PROP_1");

                entity.Property(e => e.IntProp2).HasColumnName("INT_PROP_2");

                entity.Property(e => e.LongProp1).HasColumnName("LONG_PROP_1");

                entity.Property(e => e.LongProp2).HasColumnName("LONG_PROP_2");

                entity.Property(e => e.StrProp1)
                    .HasColumnName("STR_PROP_1")
                    .HasMaxLength(512);

                entity.Property(e => e.StrProp2)
                    .HasColumnName("STR_PROP_2")
                    .HasMaxLength(512);

                entity.Property(e => e.StrProp3)
                    .HasColumnName("STR_PROP_3")
                    .HasMaxLength(512);

                entity.HasOne(d => d.QrtzTriggers)
                    .WithOne(p => p.QrtzSimpropTriggers)
                    .HasForeignKey<QrtzSimpropTriggers>(d => new { d.SchedName, d.TriggerName, d.TriggerGroup })
                    .HasConstraintName("FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS");
            });

            modelBuilder.Entity<QrtzTriggers>(entity =>
            {
                entity.HasKey(e => new { e.SchedName, e.TriggerName, e.TriggerGroup });

                entity.ToTable("QRTZ_TRIGGERS");

                entity.HasIndex(e => new { e.SchedName, e.JobName, e.JobGroup });

                entity.Property(e => e.SchedName)
                    .HasColumnName("SCHED_NAME")
                    .HasMaxLength(120);

                entity.Property(e => e.TriggerName)
                    .HasColumnName("TRIGGER_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.TriggerGroup)
                    .HasColumnName("TRIGGER_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.CalendarName)
                    .HasColumnName("CALENDAR_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(250);

                entity.Property(e => e.EndTime).HasColumnName("END_TIME");

                entity.Property(e => e.JobData)
                    .HasColumnName("JOB_DATA")
                    .HasColumnType("image");

                entity.Property(e => e.JobGroup)
                    .IsRequired()
                    .HasColumnName("JOB_GROUP")
                    .HasMaxLength(200);

                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasColumnName("JOB_NAME")
                    .HasMaxLength(200);

                entity.Property(e => e.MisfireInstr).HasColumnName("MISFIRE_INSTR");

                entity.Property(e => e.NextFireTime).HasColumnName("NEXT_FIRE_TIME");

                entity.Property(e => e.PrevFireTime).HasColumnName("PREV_FIRE_TIME");

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.StartTime).HasColumnName("START_TIME");

                entity.Property(e => e.TriggerState)
                    .IsRequired()
                    .HasColumnName("TRIGGER_STATE")
                    .HasMaxLength(16);

                entity.Property(e => e.TriggerType)
                    .IsRequired()
                    .HasColumnName("TRIGGER_TYPE")
                    .HasMaxLength(8);

                entity.HasOne(d => d.QrtzJobDetails)
                    .WithMany(p => p.QrtzTriggers)
                    .HasForeignKey(d => new { d.SchedName, d.JobName, d.JobGroup })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS");
            });

            modelBuilder.Entity<ScheduleJob>(entity =>
            {
                entity.HasKey(e => e.JobId)
                    .HasName("PK__schedule__6E32B6A5AEDFE09F");

                entity.ToTable("schedule_job");

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.BeanName)
                    .HasColumnName("bean_name")
                    .HasMaxLength(200);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CronExpression)
                    .HasColumnName("cron_expression")
                    .HasMaxLength(100);

                entity.Property(e => e.Params)
                    .HasColumnName("params")
                    .HasMaxLength(2000);

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(255);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ScheduleJobLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("PK__schedule__9E2397E03AA56FB6");

                entity.ToTable("schedule_job_log");

                entity.HasIndex(e => e.JobId)
                    .HasName("job_id");

                entity.Property(e => e.LogId).HasColumnName("log_id");

                entity.Property(e => e.BeanName)
                    .HasColumnName("bean_name")
                    .HasMaxLength(200);

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Error)
                    .HasColumnName("error")
                    .HasMaxLength(2000);

                entity.Property(e => e.JobId).HasColumnName("job_id");

                entity.Property(e => e.Params)
                    .HasColumnName("params")
                    .HasMaxLength(2000);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Times).HasColumnName("times");
            });

            modelBuilder.Entity<SysCaptcha>(entity =>
            {
                entity.HasKey(e => e.Uuid)
                    .HasName("PK__sys_capt__7F427930D52D862F");

                entity.ToTable("sys_captcha");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasMaxLength(36);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(6);

                entity.Property(e => e.ExpireTime)
                    .HasColumnName("expire_time")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<SysConfig>(entity =>
            {
                entity.ToTable("sys_config");

                entity.HasIndex(e => e.ParamKey)
                    .HasName("UQ__sys_conf__18BAEC9FE5F4F54A")
                    .IsUnique()
                    .HasFilter("([param_key] IS NOT null)");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ParamKey)
                    .HasColumnName("param_key")
                    .HasMaxLength(50);

                entity.Property(e => e.ParamValue)
                    .HasColumnName("param_value")
                    .HasMaxLength(2000);

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(500);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasData(new SysConfig()
                {
                    Id = 1,
                    ParamKey = "CLOUD_STORAGE_CONFIG_KEY",
                    ParamValue = "{\"aliyunAccessKeyId\":\"\",\"aliyunAccessKeySecret\":\"\",\"aliyunBucketName\":\"\",\"aliyunDomain\":\"\",\"aliyunEndPoint\":\"\",\"aliyunPrefix\":\"\",\"qcloudBucketName\":\"\",\"qcloudDomain\":\"\",\"qcloudPrefix\":\"\",\"qcloudSecretId\":\"\",\"qcloudSecretKey\":\"\",\"qiniuAccessKey\":\"NrgMfABZxWLo5B - YYSjoE8 - AZ1EISdi1Z3ubLOeZ\",\"qiniuBucketName\":\"ios - app\",\"qiniuDomain\":\"http://7xlij2.com1.z0.glb.clouddn.com\",\"qiniuPrefix\":\"upload\",\"qiniuSecretKey\":\"uIwJHevMRWU0VLxFvgy0tAcOdGqasdtVlJkdy6vV\",\"type\":1}",
                    Status = 0,
                    Remark = "云存储配置信息"
                });
            });

            modelBuilder.Entity<SysLog>(entity =>
            {
                entity.ToTable("sys_log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasColumnName("ip")
                    .HasMaxLength(64);

                entity.Property(e => e.Method)
                    .HasColumnName("method")
                    .HasMaxLength(200);

                entity.Property(e => e.Operation)
                    .HasColumnName("operation")
                    .HasMaxLength(50);

                entity.Property(e => e.Params)
                    .HasColumnName("params")
                    .HasMaxLength(4000);

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK__sys_menu__4CA0FADCF95CB900");

                entity.ToTable("sys_menu");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.OrderNum).HasColumnName("order_num");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.Perms)
                    .HasColumnName("perms")
                    .HasMaxLength(500);

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(200);

                entity.HasData(
                    new SysMenu(1, 0, "系统管理", null, null, 0, "system", 0),
                    new SysMenu(2, 1, "管理员列表", "sys/user", null, 1, "admin", 1),
                    new SysMenu(3, 1, "角色管理", "sys/role", null, 1, "role", 2),
                    new SysMenu(4, 1, "菜单管理", "sys/menu", null, 1, "menu", 3),
                    new SysMenu(5, 1, "SQL监控", "http://localhost:8080/renren-fast/druid/sql.html", null, 1, "sql", 4),
                    new SysMenu(6, 1, "定时任务", "job/schedule", null, 1, "job", 5),
                    new SysMenu(7, 6, "查看", null, "sys:schedule:list,sys:schedule:info", 2, null, 0),
                    new SysMenu(8, 6, "新增", null, "sys:schedule:save", 2, null, 0),
                    new SysMenu(9, 6, "修改", null, "sys:schedule:update", 2, null, 0),
                    new SysMenu(10, 6, "删除", null, "sys:schedule:delete", 2, null, 0),
                    new SysMenu(11, 6, "暂停", null, "sys:schedule:pause", 2, null, 0),
                    new SysMenu(12, 6, "恢复", null, "sys:schedule:resume", 2, null, 0),
                    new SysMenu(13, 6, "立即执行", null, "sys:schedule:run", 2, null, 0),
                    new SysMenu(14, 6, "日志列表", null, "sys:schedule:log", 2, null, 0),
                    new SysMenu(15, 2, "查看", null, "sys:user:list,sys:user:info", 2, null, 0),
                    new SysMenu(16, 2, "新增", null, "sys:user:save,sys:role:select", 2, null, 0),
                    new SysMenu(17, 2, "修改", null, "sys:user:update,sys:role:select", 2, null, 0),
                    new SysMenu(18, 2, "删除", null, "sys:user:delete", 2, null, 0),
                    new SysMenu(19, 3, "查看", null, "sys:role:list,sys:role:info", 2, null, 0),
                    new SysMenu(20, 3, "新增", null, "sys:role:save,sys:menu:list", 2, null, 0),
                    new SysMenu(21, 3, "修改", null, "sys:role:update,sys:menu:list", 2, null, 0),
                    new SysMenu(22, 3, "删除", null, "sys:role:delete", 2, null, 0),
                    new SysMenu(23, 4, "查看", null, "sys:menu:list,sys:menu:info", 2, null, 0),
                    new SysMenu(24, 4, "新增", null, "sys:menu:save,sys:menu:select", 2, null, 0),
                    new SysMenu(25, 4, "修改", null, "sys:menu:update,sys:menu:select", 2, null, 0),
                    new SysMenu(26, 4, "删除", null, "sys:menu:delete", 2, null, 0),
                    new SysMenu(27, 1, "参数管理", "sys/config", "sys:config:list,sys:config:info,sys:config:save,sys:config:update,sys:config:delete", 1, "config", 6),
                    new SysMenu(29, 1, "系统日志", "sys/log", "sys:log:list", 1, "log", 7),
                    new SysMenu(30, 1, "文件上传", "oss/oss", "sys:oss:all", 1, "oss", 6)
                    );
            });

            modelBuilder.Entity<SysOss>(entity =>
            {
                entity.ToTable("sys_oss");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__sys_role__760965CCC85E5C67");

                entity.ToTable("sys_role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("create_user_id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Remark)
                    .HasColumnName("remark")
                    .HasMaxLength(100);

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SysRoleMenu>(entity =>
            {
                entity.ToTable("sys_role_menu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MenuId).HasColumnName("menu_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__sys_user__B9BE370FDFCC32AC");

                entity.ToTable("sys_user");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__sys_user__F3DBC57245BB95AA")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreateUserId)
                    .HasColumnName("create_user_id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(100);

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasMaxLength(20);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.HasData(new SysUser()
                {
                    UserId = "00000000000000000000000000000000",
                    Username = "admin",
                    Password = "d97e62ea2a6b61a8b957b52a6e39f978fc4aa562103ae04353d3a9f051ea85cf",
                    Salt = "YzcmCZNvbXocrsz9dm8e",
                    Email = "coolyuwk@live.com",
                    Mobile = "18888888888",
                    Status = 1,
                    CreateUserId = "1",
                    CreateTime = DateTime.Now
                });
            });

            modelBuilder.Entity<SysUserRole>(entity =>
            {
                entity.ToTable("sys_user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SysUserToken>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__sys_user__B9BE370FED885F1F");

                entity.ToTable("sys_user_token");

                entity.HasIndex(e => e.Token)
                    .HasName("UQ__sys_user__CA90DA7AB4305286")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ExpireTime)
                    .HasColumnName("expire_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("update_time")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
