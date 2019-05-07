using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WuMortal.Dmhy.DataAnalysis.Models;

namespace WuMortal.Dmhy.DataAnalysis.Models
{
    /// <summary>
    /// 帖子详细
    /// </summary>
    public class DPostDetailed
    {
        public DPostDetailedUser User { get; set; }

        public DPostDetailedTeam Team { get; set; }

        public DPost Post { get; set; }

        /// <summary>
        /// 帖子内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// BT列表
        /// </summary>
        public Object[] BTDict { get; set; } = { };

        public Object[] BTContentDict { get; set; } = { };

    }

    public class DPostDetailedUser
    {
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatarImg { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }

    public class DPostDetailedTeam
    {
        /// <summary>
        /// 字幕组头像
        /// </summary>
        public string TeamLogoUrl { get; set; }
        /// <summary>
        /// 字幕组名称
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 字幕组Id
        /// </summary>
        public string TeamId { get; set; }
    }
}
