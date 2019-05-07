using System;
using System.Collections.Generic;
using System.Text;

namespace WuMortal.Dmhy.DataAnalysis.Models
{
    public class DComment
    {
        public long Id { get; set; }

        public DCommentUserInfo UserInfo { get; set; }

        /// <summary>
        /// 评论类容
        /// </summary>
        public string Content { get; set; }
    }

    public class DCommentUserInfo
    {
        /// <summary>
        /// 发表人姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 发表日期
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        /// 发表人IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 头像URL
        /// </summary>
        public string ProfilePictureUrl { get; set; }
    }
}
