using System;
using System.Collections.Generic;
using System.Text;

namespace RenRen.Domain.Common.Utils
{
    public partial class Constant
    {
        /// <summary>
        /// 验证规则
        /// </summary>
        public class ValidatorGroup
        {
            /// <summary>
            /// 添加
            /// </summary>
            public const string Add = "AddGroup";
            /// <summary>
            /// 删除
            /// </summary>
            public const string Update = "UpdateGroup";
        }

        /** 超级管理员ID */
        public static string SUPER_ADMIN = "00000000000000000000000000000000";
        /**
         * 当前页码
         */
        public static string PAGE = "page";
        /**
         * 每页显示记录数
         */
        public static string LIMIT = "limit";
        /**
         * 排序字段
         */
        public static string ORDER_FIELD = "sidx";
        /**
         * 排序方式
         */
        public static string ORDER = "order";
        /**
         *  升序
         */
        public static string ASC = "asc";
        /**
         * 菜单类型
         * 
         * @author chenshun
         * @email sunlightcs@gmail.com
         * @date 2016年11月15日 下午1:24:29
         */
        public enum MenuType
        {
            /**
             * 目录
             */
            目录,
            /**
             * 菜单
             */
            菜单,
            /**
             * 按钮
             */
            按钮
        }

        /**
         * 定时任务状态
         * 
         * @author chenshun
         * @email sunlightcs@gmail.com
         * @date 2016年12月3日 上午12:07:22
         */
        public enum ScheduleStatus
        {
            /**
             * 正常
             */
            正常,
            /**
             * 暂停
             */
            暂停
        }



        /**
         * 云服务商
         */
        public enum CloudService
        {
            /**
             * 七牛云
             */
            七牛云 = 1,
            /**
             * 阿里云
             */
            阿里云 = 2,
            /**
             * 腾讯云
             */
            腾讯云 = 3
        }
    }
}