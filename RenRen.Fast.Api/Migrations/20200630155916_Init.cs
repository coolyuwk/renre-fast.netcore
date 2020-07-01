using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RenRen.Fast.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_user",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 32, nullable: false),
                    mail = table.Column<string>(maxLength: 64, nullable: true, comment: "邮箱"),
                    user_name = table.Column<string>(maxLength: 128, nullable: true, comment: "显示用户名"),
                    mobile = table.Column<string>(maxLength: 16, nullable: true, comment: "手机,登陆帐号"),
                    password = table.Column<string>(maxLength: 512, nullable: true),
                    status = table.Column<int>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 32, nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    delete_time = table.Column<DateTime>(nullable: true),
                    delete_user = table.Column<string>(maxLength: 32, nullable: true),
                    logs = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "msg_sms",
                columns: table => new
                {
                    id = table.Column<string>(maxLength: 32, nullable: false),
                    content = table.Column<string>(maxLength: 80, nullable: false, comment: "短信内容"),
                    send_time = table.Column<DateTime>(nullable: false),
                    expired_time = table.Column<DateTime>(nullable: false, comment: "过期时间"),
                    mobile = table.Column<string>(maxLength: 16, nullable: false),
                    msg_type = table.Column<int>(nullable: false, comment: "短信类型：注册 = 0"),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<string>(maxLength: 32, nullable: true),
                    deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_msg_sms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_BLOB_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    TRIGGER_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    TRIGGER_GROUP = table.Column<string>(maxLength: 200, nullable: false),
                    BLOB_DATA = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_CALENDARS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    CALENDAR_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    CALENDAR = table.Column<byte[]>(type: "image", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_CALENDARS", x => new { x.SCHED_NAME, x.CALENDAR_NAME });
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_FIRED_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    ENTRY_ID = table.Column<string>(maxLength: 95, nullable: false),
                    TRIGGER_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    TRIGGER_GROUP = table.Column<string>(maxLength: 200, nullable: false),
                    INSTANCE_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    FIRED_TIME = table.Column<long>(nullable: false),
                    SCHED_TIME = table.Column<long>(nullable: false),
                    PRIORITY = table.Column<int>(nullable: false),
                    STATE = table.Column<string>(maxLength: 16, nullable: false),
                    JOB_NAME = table.Column<string>(maxLength: 200, nullable: true),
                    JOB_GROUP = table.Column<string>(maxLength: 200, nullable: true),
                    IS_NONCONCURRENT = table.Column<string>(maxLength: 1, nullable: true),
                    REQUESTS_RECOVERY = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_FIRED_TRIGGERS", x => new { x.SCHED_NAME, x.ENTRY_ID });
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_JOB_DETAILS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    JOB_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    JOB_GROUP = table.Column<string>(maxLength: 200, nullable: false),
                    DESCRIPTION = table.Column<string>(maxLength: 250, nullable: true),
                    JOB_CLASS_NAME = table.Column<string>(maxLength: 250, nullable: false),
                    IS_DURABLE = table.Column<string>(maxLength: 1, nullable: false),
                    IS_NONCONCURRENT = table.Column<string>(maxLength: 1, nullable: false),
                    IS_UPDATE_DATA = table.Column<string>(maxLength: 1, nullable: false),
                    REQUESTS_RECOVERY = table.Column<string>(maxLength: 1, nullable: false),
                    JOB_DATA = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_JOB_DETAILS", x => new { x.SCHED_NAME, x.JOB_NAME, x.JOB_GROUP });
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_LOCKS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    LOCK_NAME = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_LOCKS", x => new { x.SCHED_NAME, x.LOCK_NAME });
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_PAUSED_TRIGGER_GRPS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    TRIGGER_GROUP = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_PAUSED_TRIGGER_GRPS", x => new { x.SCHED_NAME, x.TRIGGER_GROUP });
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_SCHEDULER_STATE",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    INSTANCE_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    LAST_CHECKIN_TIME = table.Column<long>(nullable: false),
                    CHECKIN_INTERVAL = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_SCHEDULER_STATE", x => new { x.SCHED_NAME, x.INSTANCE_NAME });
                });

            migrationBuilder.CreateTable(
                name: "schedule_job",
                columns: table => new
                {
                    job_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bean_name = table.Column<string>(maxLength: 200, nullable: true),
                    @params = table.Column<string>(name: "params", maxLength: 2000, nullable: true),
                    cron_expression = table.Column<string>(maxLength: 100, nullable: true),
                    status = table.Column<byte>(nullable: true),
                    remark = table.Column<string>(maxLength: 255, nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__schedule__6E32B6A5AEDFE09F", x => x.job_id);
                });

            migrationBuilder.CreateTable(
                name: "schedule_job_log",
                columns: table => new
                {
                    log_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_id = table.Column<long>(nullable: false),
                    bean_name = table.Column<string>(maxLength: 200, nullable: true),
                    @params = table.Column<string>(name: "params", maxLength: 2000, nullable: true),
                    status = table.Column<byte>(nullable: false),
                    error = table.Column<string>(maxLength: 2000, nullable: true),
                    times = table.Column<int>(nullable: false),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__schedule__9E2397E03AA56FB6", x => x.log_id);
                });

            migrationBuilder.CreateTable(
                name: "sys_captcha",
                columns: table => new
                {
                    uuid = table.Column<string>(maxLength: 36, nullable: false),
                    code = table.Column<string>(maxLength: 6, nullable: false),
                    expire_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sys_capt__7F427930D52D862F", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "sys_config",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    param_key = table.Column<string>(maxLength: 50, nullable: true),
                    param_value = table.Column<string>(maxLength: 2000, nullable: true),
                    status = table.Column<byte>(nullable: true, defaultValueSql: "((1))"),
                    remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_config", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_log",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(maxLength: 50, nullable: true),
                    operation = table.Column<string>(maxLength: 50, nullable: true),
                    method = table.Column<string>(maxLength: 200, nullable: true),
                    @params = table.Column<string>(name: "params", maxLength: 4000, nullable: true),
                    time = table.Column<long>(nullable: false),
                    ip = table.Column<string>(maxLength: 64, nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_menu",
                columns: table => new
                {
                    menu_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    parent_id = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 50, nullable: true),
                    url = table.Column<string>(maxLength: 200, nullable: true),
                    perms = table.Column<string>(maxLength: 500, nullable: true),
                    type = table.Column<int>(nullable: true),
                    icon = table.Column<string>(maxLength: 50, nullable: true),
                    order_num = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sys_menu__4CA0FADCF95CB900", x => x.menu_id);
                });

            migrationBuilder.CreateTable(
                name: "sys_oss",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(maxLength: 200, nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_oss", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_role",
                columns: table => new
                {
                    role_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(maxLength: 100, nullable: true),
                    remark = table.Column<string>(maxLength: 100, nullable: true),
                    create_user_id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 32, nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sys_role__760965CCC85E5C67", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "sys_role_menu",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<long>(nullable: true),
                    menu_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_role_menu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_user",
                columns: table => new
                {
                    user_id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 32, nullable: false),
                    username = table.Column<string>(maxLength: 50, nullable: false),
                    password = table.Column<string>(maxLength: 100, nullable: true),
                    salt = table.Column<string>(maxLength: 20, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    mobile = table.Column<string>(maxLength: 100, nullable: true),
                    status = table.Column<byte>(nullable: true),
                    create_user_id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 32, nullable: true),
                    create_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sys_user__B9BE370FDFCC32AC", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "sys_user_role",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 32, nullable: true),
                    role_id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sys_user_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sys_user_token",
                columns: table => new
                {
                    user_id = table.Column<string>(unicode: false, fixedLength: true, maxLength: 32, nullable: false),
                    token = table.Column<string>(maxLength: 100, nullable: false),
                    expire_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_time = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__sys_user__B9BE370FED885F1F", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    TRIGGER_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    TRIGGER_GROUP = table.Column<string>(maxLength: 200, nullable: false),
                    JOB_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    JOB_GROUP = table.Column<string>(maxLength: 200, nullable: false),
                    DESCRIPTION = table.Column<string>(maxLength: 250, nullable: true),
                    NEXT_FIRE_TIME = table.Column<long>(nullable: true),
                    PREV_FIRE_TIME = table.Column<long>(nullable: true),
                    PRIORITY = table.Column<int>(nullable: true),
                    TRIGGER_STATE = table.Column<string>(maxLength: 16, nullable: false),
                    TRIGGER_TYPE = table.Column<string>(maxLength: 8, nullable: false),
                    START_TIME = table.Column<long>(nullable: false),
                    END_TIME = table.Column<long>(nullable: true),
                    CALENDAR_NAME = table.Column<string>(maxLength: 200, nullable: true),
                    MISFIRE_INSTR = table.Column<short>(nullable: true),
                    JOB_DATA = table.Column<byte[]>(type: "image", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS",
                        columns: x => new { x.SCHED_NAME, x.JOB_NAME, x.JOB_GROUP },
                        principalTable: "QRTZ_JOB_DETAILS",
                        principalColumns: new[] { "SCHED_NAME", "JOB_NAME", "JOB_GROUP" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_CRON_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    TRIGGER_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    TRIGGER_GROUP = table.Column<string>(maxLength: 200, nullable: false),
                    CRON_EXPRESSION = table.Column<string>(maxLength: 120, nullable: false),
                    TIME_ZONE_ID = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_CRON_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS",
                        columns: x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP },
                        principalTable: "QRTZ_TRIGGERS",
                        principalColumns: new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_SIMPLE_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    TRIGGER_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    TRIGGER_GROUP = table.Column<string>(maxLength: 200, nullable: false),
                    REPEAT_COUNT = table.Column<long>(nullable: false),
                    REPEAT_INTERVAL = table.Column<long>(nullable: false),
                    TIMES_TRIGGERED = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_SIMPLE_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS",
                        columns: x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP },
                        principalTable: "QRTZ_TRIGGERS",
                        principalColumns: new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QRTZ_SIMPROP_TRIGGERS",
                columns: table => new
                {
                    SCHED_NAME = table.Column<string>(maxLength: 120, nullable: false),
                    TRIGGER_NAME = table.Column<string>(maxLength: 200, nullable: false),
                    TRIGGER_GROUP = table.Column<string>(maxLength: 200, nullable: false),
                    STR_PROP_1 = table.Column<string>(maxLength: 512, nullable: true),
                    STR_PROP_2 = table.Column<string>(maxLength: 512, nullable: true),
                    STR_PROP_3 = table.Column<string>(maxLength: 512, nullable: true),
                    INT_PROP_1 = table.Column<int>(nullable: true),
                    INT_PROP_2 = table.Column<int>(nullable: true),
                    LONG_PROP_1 = table.Column<long>(nullable: true),
                    LONG_PROP_2 = table.Column<long>(nullable: true),
                    DEC_PROP_1 = table.Column<decimal>(type: "numeric(13, 4)", nullable: true),
                    DEC_PROP_2 = table.Column<decimal>(type: "numeric(13, 4)", nullable: true),
                    BOOL_PROP_1 = table.Column<string>(maxLength: 1, nullable: true),
                    BOOL_PROP_2 = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRTZ_SIMPROP_TRIGGERS", x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP });
                    table.ForeignKey(
                        name: "FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS",
                        columns: x => new { x.SCHED_NAME, x.TRIGGER_NAME, x.TRIGGER_GROUP },
                        principalTable: "QRTZ_TRIGGERS",
                        principalColumns: new[] { "SCHED_NAME", "TRIGGER_NAME", "TRIGGER_GROUP" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "sys_config",
                columns: new[] { "id", "param_key", "param_value", "remark", "status" },
                values: new object[] { 1L, "CLOUD_STORAGE_CONFIG_KEY", "{\"aliyunAccessKeyId\":\"\",\"aliyunAccessKeySecret\":\"\",\"aliyunBucketName\":\"\",\"aliyunDomain\":\"\",\"aliyunEndPoint\":\"\",\"aliyunPrefix\":\"\",\"qcloudBucketName\":\"\",\"qcloudDomain\":\"\",\"qcloudPrefix\":\"\",\"qcloudSecretId\":\"\",\"qcloudSecretKey\":\"\",\"qiniuAccessKey\":\"NrgMfABZxWLo5B - YYSjoE8 - AZ1EISdi1Z3ubLOeZ\",\"qiniuBucketName\":\"ios - app\",\"qiniuDomain\":\"http://7xlij2.com1.z0.glb.clouddn.com\",\"qiniuPrefix\":\"upload\",\"qiniuSecretKey\":\"uIwJHevMRWU0VLxFvgy0tAcOdGqasdtVlJkdy6vV\",\"type\":1}", "云存储配置信息", (byte)0 });

            migrationBuilder.InsertData(
                table: "sys_menu",
                columns: new[] { "menu_id", "icon", "name", "order_num", "parent_id", "perms", "type", "url" },
                values: new object[,]
                {
                    { 29L, "log", "系统日志", 7, 1L, "sys:log:list", 1, "sys/log" },
                    { 27L, "config", "参数管理", 6, 1L, "sys:config:list,sys:config:info,sys:config:save,sys:config:update,sys:config:delete", 1, "sys/config" },
                    { 26L, null, "删除", 0, 4L, "sys:menu:delete", 2, null },
                    { 25L, null, "修改", 0, 4L, "sys:menu:update,sys:menu:select", 2, null },
                    { 24L, null, "新增", 0, 4L, "sys:menu:save,sys:menu:select", 2, null },
                    { 23L, null, "查看", 0, 4L, "sys:menu:list,sys:menu:info", 2, null },
                    { 22L, null, "删除", 0, 3L, "sys:role:delete", 2, null },
                    { 21L, null, "修改", 0, 3L, "sys:role:update,sys:menu:list", 2, null },
                    { 20L, null, "新增", 0, 3L, "sys:role:save,sys:menu:list", 2, null },
                    { 19L, null, "查看", 0, 3L, "sys:role:list,sys:role:info", 2, null },
                    { 18L, null, "删除", 0, 2L, "sys:user:delete", 2, null },
                    { 17L, null, "修改", 0, 2L, "sys:user:update,sys:role:select", 2, null },
                    { 16L, null, "新增", 0, 2L, "sys:user:save,sys:role:select", 2, null },
                    { 30L, "oss", "文件上传", 6, 1L, "sys:oss:all", 1, "oss/oss" },
                    { 15L, null, "查看", 0, 2L, "sys:user:list,sys:user:info", 2, null },
                    { 13L, null, "立即执行", 0, 6L, "sys:schedule:run", 2, null },
                    { 12L, null, "恢复", 0, 6L, "sys:schedule:resume", 2, null },
                    { 11L, null, "暂停", 0, 6L, "sys:schedule:pause", 2, null },
                    { 10L, null, "删除", 0, 6L, "sys:schedule:delete", 2, null },
                    { 9L, null, "修改", 0, 6L, "sys:schedule:update", 2, null },
                    { 8L, null, "新增", 0, 6L, "sys:schedule:save", 2, null },
                    { 7L, null, "查看", 0, 6L, "sys:schedule:list,sys:schedule:info", 2, null },
                    { 6L, "job", "定时任务", 5, 1L, null, 1, "job/schedule" },
                    { 5L, "sql", "SQL监控", 4, 1L, null, 1, "http://localhost:8080/renren-fast/druid/sql.html" },
                    { 4L, "menu", "菜单管理", 3, 1L, null, 1, "sys/menu" },
                    { 3L, "role", "角色管理", 2, 1L, null, 1, "sys/role" },
                    { 2L, "admin", "管理员列表", 1, 1L, null, 1, "sys/user" },
                    { 1L, "system", "系统管理", 0, 0L, null, 0, null },
                    { 14L, null, "日志列表", 0, 6L, "sys:schedule:log", 2, null }
                });

            migrationBuilder.InsertData(
                table: "sys_user",
                columns: new[] { "user_id", "create_time", "create_user_id", "email", "mobile", "password", "salt", "status", "username" },
                values: new object[] { "00000000000000000000000000000000", new DateTime(2020, 6, 30, 23, 59, 16, 444, DateTimeKind.Local).AddTicks(6816), "1", "coolyuwk@live.com", "18888888888", "d97e62ea2a6b61a8b957b52a6e39f978fc4aa562103ae04353d3a9f051ea85cf", "YzcmCZNvbXocrsz9dm8e", (byte)1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_QRTZ_TRIGGERS_SCHED_NAME_JOB_NAME_JOB_GROUP",
                table: "QRTZ_TRIGGERS",
                columns: new[] { "SCHED_NAME", "JOB_NAME", "JOB_GROUP" });

            migrationBuilder.CreateIndex(
                name: "job_id",
                table: "schedule_job_log",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "UQ__sys_conf__18BAEC9FE5F4F54A",
                table: "sys_config",
                column: "param_key",
                unique: true,
                filter: "([param_key] IS NOT null)");

            migrationBuilder.CreateIndex(
                name: "UQ__sys_user__F3DBC57245BB95AA",
                table: "sys_user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__sys_user__CA90DA7AB4305286",
                table: "sys_user_token",
                column: "token",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_user");

            migrationBuilder.DropTable(
                name: "msg_sms");

            migrationBuilder.DropTable(
                name: "QRTZ_BLOB_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_CALENDARS");

            migrationBuilder.DropTable(
                name: "QRTZ_CRON_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_FIRED_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_LOCKS");

            migrationBuilder.DropTable(
                name: "QRTZ_PAUSED_TRIGGER_GRPS");

            migrationBuilder.DropTable(
                name: "QRTZ_SCHEDULER_STATE");

            migrationBuilder.DropTable(
                name: "QRTZ_SIMPLE_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_SIMPROP_TRIGGERS");

            migrationBuilder.DropTable(
                name: "schedule_job");

            migrationBuilder.DropTable(
                name: "schedule_job_log");

            migrationBuilder.DropTable(
                name: "sys_captcha");

            migrationBuilder.DropTable(
                name: "sys_config");

            migrationBuilder.DropTable(
                name: "sys_log");

            migrationBuilder.DropTable(
                name: "sys_menu");

            migrationBuilder.DropTable(
                name: "sys_oss");

            migrationBuilder.DropTable(
                name: "sys_role");

            migrationBuilder.DropTable(
                name: "sys_role_menu");

            migrationBuilder.DropTable(
                name: "sys_user");

            migrationBuilder.DropTable(
                name: "sys_user_role");

            migrationBuilder.DropTable(
                name: "sys_user_token");

            migrationBuilder.DropTable(
                name: "QRTZ_TRIGGERS");

            migrationBuilder.DropTable(
                name: "QRTZ_JOB_DETAILS");
        }
    }
}
