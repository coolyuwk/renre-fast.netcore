using System;
using System.Collections.Generic;
using System.Linq;

namespace RenRen.Domain.Common.Utils
{
    public class PageUtils<T>
    {
        /**
		 * 总记录数
		 */
        public int TotalCount;
        /**
		 * 每页记录数
		 */
        public int PageSize;
        /**
		 * 总页数
		 */
        public int TotalPage;
        /**
		 * 当前页数
		 */
        public int CurrPage;
        /**
		 * 列表数据
		 */
        public List<T> List;

        /**
		 * 分页
		 * @param list        列表数据
		 * @param totalCount  总记录数
		 * @param pageSize    每页记录数
		 * @param currPage    当前页数
		 */
        public PageUtils(List<T> list, int totalCount, int pageSize, int currPage)
        {
            this.List = list;
            this.TotalCount = totalCount;
            this.PageSize = pageSize;
            this.CurrPage = currPage;
            this.TotalPage = (int)Math.Ceiling((double)totalCount / pageSize);
        }

        /**
		 * 分页
		 */
        public PageUtils(IPage page, IQueryable<T> query)
        {
            this.List = query.Skip((page.Page - 1) * page.Limit).Take(page.Limit).ToList();
            this.TotalCount = query.Count();
            this.PageSize = page.Limit;
            this.CurrPage = page.Page;
            this.TotalPage = (int)Math.Ceiling((double)this.TotalCount / this.PageSize);
        }
    }

    public interface IPage
    {
        int Page { get; set; }
        int Limit { get; set; }
    }


    public interface ITime
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; set; }
        //结束时间
        DateTime EndTime { get; set; }
    }
}
