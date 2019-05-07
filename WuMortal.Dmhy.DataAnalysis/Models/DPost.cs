using System;
using System.Collections.Generic;
using System.Text;
using WuMortal.Dmhy.DataAnalysis.Models.Models;

namespace WuMortal.Dmhy.DataAnalysis.Models
{
    public class DPost
    {
        public string Id { get; set; }

        public long UserId { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public string DateTime { get; set; }

        public DCategory Category { get; set; }

        public DTeam Team { get; set; }

        /// <summary>
        /// 番剧标题
        /// </summary>
        public string Title { get; set; }

        public string HtmlId { get; set; }

        /// <summary>
        /// 种子下载地址
        /// </summary>
        public string DownloadArrow { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }
        public string UserName { get; internal set; }
    }
}
