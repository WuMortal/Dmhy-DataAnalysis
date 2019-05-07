using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WuMortal.Dmhy.DataAnalysis.Models;
using WuMortal.Dmhy.DataAnalysis.Models.Models;

namespace WuMortal.Dmhy.DataAnalysis.Interface
{
    public interface IDmhyInfo
    {
        /// <summary>
        /// 获取番剧索引数据
        /// </summary>
        /// <returns></returns>
        Task<DDramaIndex[]> GetDramaIndexDataAsync();

        /// <summary>
        /// 获取番剧类别信息
        /// </summary>
        /// <returns></returns>
        Task<DCategory[]> GetDramaCategoryAsync();


        /// <summary>
        /// 获取字幕组信息
        /// </summary>
        /// <returns></returns>
        Task<DTeam[]> GetTeamAsync();

        /// <summary>
        /// 热门资源
        /// </summary>
        /// <returns></returns>
        Task<DHotPost[]> GetHotPostAsync();
    }
}
