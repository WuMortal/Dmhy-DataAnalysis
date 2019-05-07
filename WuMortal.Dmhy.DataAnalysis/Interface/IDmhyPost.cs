using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WuMortal.Dmhy.DataAnalysis.Models;
using WuMortal.Dmhy.DataAnalysis.Models.Models;

namespace WuMortal.Dmhy.DataAnalysis.Interface
{
    public interface IDmhyPost
    {
        /// <summary>
        /// 根据分类Id获取帖子
        /// </summary>
        /// <param name="categoryId">类别Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        Task<DPost[]> GetTopsDataByCategoryIdAsync(long categoryId, long pageIndex);

        /// <summary>
        /// 根据关键词获取帖子
        /// </summary>
        /// <param name="keyWord">搜索的关键词</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        Task<DPost[]> GetTopsDataByKeyWordAsync(string keyWord, long pageIndex);

        /// <summary>
        /// 获取最新帖子
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        Task<DPost[]> GetTopsDataByPageIndexAsync(long pageIndex);

        /// <summary>
        /// 获取字幕组的帖子
        /// </summary>
        /// <param name="userId">字幕组Id</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<DPost[]> GetTopsDataByTeamIdAsync(long userId, long pageIndex);

        /// <summary>
        /// 获取用户发布的帖子
        /// </summary>
        /// <param name="userId">字幕组Id</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<DPost[]> GetTopsDataByUserIdAsync(long userId, long pageIndex);
    }
}
