using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WuMortal.Dmhy.DataAnalysis.Models;

namespace WuMortal.Dmhy.DataAnalysis.Interface
{
    public interface IDmhyPostDetailed
    {
        /// <summary>
        /// 获取帖子详细内容
        /// </summary>
        /// <param name="id">帖子Id</param>
        /// <returns></returns>
        Task<DPostDetailed> GetPostDetailedAsync(string id);

        /// <summary>
        /// 获取帖子的评论信息
        /// </summary>
        /// <param name="postId">帖子Id</param>
        /// <returns></returns>
        Task<DComment[]> GetCommentsAsync(string postId);
    }
}
