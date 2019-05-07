using Microsoft.VisualStudio.TestTools.UnitTesting;
using WuMortal.Dmhy.DataAnalysis.Core;
using WuMortal.Dmhy.DataAnalysis.Interface;

namespace WuMortal.Dmhy.UnitTest
{
    [TestClass]
    public class UnitTestDmhyPost
    {
        readonly IDmhyPost _dmhyPost;

        public UnitTestDmhyPost()
        {
            DmhyFactory dmhyFactory = new DmhyFactory(new System.Net.Http.HttpClient());
            _dmhyPost = dmhyFactory.BuilderDmhyPost();
        }

        [TestMethod]
        public void TestGetTopsDataByCategoryIdAsync()
        {
            var result = _dmhyPost.GetTopsDataByCategoryIdAsync(2, 1).Result;
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestGetTopsDataByKeyWordAsync()
        {
            var result = _dmhyPost.GetTopsDataByKeyWordAsync("؈؈�ձ�ʷ", 1).Result;
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestGetTopsDataByTeamIdAsync()
        {
            var result = _dmhyPost.GetTopsDataByTeamIdAsync(604, 1).Result;
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void TestGetTopsDataByUserIdAsync()
        {
            var result = _dmhyPost.GetTopsDataByUserIdAsync(55561, 1).Result;
            Assert.IsTrue(result.Length > 0);
        }


        [TestMethod]
        public void TestGetTopsDataByPageIndexAsync()
        {
            var result = _dmhyPost.GetTopsDataByPageIndexAsync(1).Result;
            Assert.IsTrue(result.Length > 0);
        }

    }
}
