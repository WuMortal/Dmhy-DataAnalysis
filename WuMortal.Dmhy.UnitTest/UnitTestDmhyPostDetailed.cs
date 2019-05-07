using Microsoft.VisualStudio.TestTools.UnitTesting;
using WuMortal.Dmhy.DataAnalysis.Core;
using WuMortal.Dmhy.DataAnalysis.Interface;

namespace WuMortal.Dmhy.UnitTest
{
    [TestClass]
    public class UnitTestDmhyPostDetailed
    {
        readonly IDmhyPostDetailed _dmhyDPostDetailed;

        public UnitTestDmhyPostDetailed()
        {
            DmhyFactory dmhyFactory = new DmhyFactory(new System.Net.Http.HttpClient());
            _dmhyDPostDetailed = dmhyFactory.BuilderDmhyPostDetailed();
        }

        [TestMethod]
        public void TestGetPostDetailedAsync()
        {
            var result = _dmhyDPostDetailed.GetPostDetailedAsync("516366_c_c_4_Shingeki_no_Kyojin_S3_14_BIG5_1080P_MP4").Result;
            Assert.IsNotNull(result?.User);
        }

        [TestMethod]
        public void TestGetCommentsAsync()
        {
            var result = _dmhyDPostDetailed.GetCommentsAsync("516366_c_c_4_Shingeki_no_Kyojin_S3_14_BIG5_1080P_MP4").Result;
            Assert.IsNotNull(result);
        }


    }
}
