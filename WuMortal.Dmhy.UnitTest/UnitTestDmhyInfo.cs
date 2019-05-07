using Microsoft.VisualStudio.TestTools.UnitTesting;
using WuMortal.Dmhy.DataAnalysis.Core;
using WuMortal.Dmhy.DataAnalysis.Interface;

namespace WuMortal.Dmhy.UnitTest
{
    [TestClass]
    public class UnitTestDmhyInfo
    {
        readonly IDmhyInfo _dmhyInfo;

        public UnitTestDmhyInfo()
        {
            DmhyFactory dmhyFactory = new DmhyFactory(new System.Net.Http.HttpClient());
            _dmhyInfo = dmhyFactory.BuilderDmhyInfo();
        }

        [TestMethod]
        public void TestGetDramaIndexDataAsync()
        {
            var dramaIndexData = _dmhyInfo.GetDramaIndexDataAsync().Result;
            Assert.IsTrue(dramaIndexData.Length > 0);
        }

        [TestMethod]
        public void TestGetDramaCategoryAsync()
        {
            var dramaIndexData = _dmhyInfo.GetDramaCategoryAsync().Result;
            Assert.IsTrue(dramaIndexData.Length > 0);
        }

        [TestMethod]
        public void TestGetHotPostAsync()
        {
            var dramaIndexData = _dmhyInfo.GetHotPostAsync().Result;
            Assert.IsTrue(dramaIndexData.Length > 0);
        }

        [TestMethod]
        public void TestGetTeamAsync()
        {
            var dramaIndexData = _dmhyInfo.GetTeamAsync().Result;
            Assert.IsTrue(dramaIndexData.Length > 0);
        }

    }
}
