using System.Collections.Generic;
using System.Linq;
using ConsoleApp2;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleAppUnitTest
{
    [TestClass]
    public class ConvertUtilsTest
    {
        private RootObject _rootObj;

        [TestInitialize]
        public void TestInitialize()
        {
            _rootObj = new RootObject
            {
                results = new List<Character.Result>()
                {
                    new Character.Result
                    {
                        starships = new List<string> {"X-Wing"}
                    }
                }
            };
        }


        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void ConvertToCharacterThrowsWhenNullArgTest()
        {
            ConvertUtils.ConvertToCharacters(null);
        }

        [TestMethod]
        public void ConvertToCharacterReturnsEmptyWhenNoResultTest()
        {
            _rootObj.results = new List<Character.Result>();

            var res = ConvertUtils.ConvertToCharacters(_rootObj);

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void ConvertToCharacterReturnsEmptyWhenNoStarshipsTest()
        {
            _rootObj.results.First().starships = new List<string>();

            var res = ConvertUtils.ConvertToCharacters(_rootObj);

            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void ConvertToCharacterSetsNameTest()
        {
            var namn = "KalleTest";
            _rootObj.results.First().name = namn;

            var res = ConvertUtils.ConvertToCharacters(_rootObj);

            Assert.AreEqual(namn, res.First().Name);
        }

    }
}
