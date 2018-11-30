using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuMortal.Dmhy.Models
{
    /// <summary>
    /// 用于存放每个星期的番剧索引
    /// </summary>
    public class DDrama
    {
        public string Id { get; set; }

        /// <summary>
        /// 所属星期的所有番剧
        /// </summary>
        public string Names { get; set; }

        public string ImgSrc { get; set; }

        public string KeyWord { get; set; }
    }
}
