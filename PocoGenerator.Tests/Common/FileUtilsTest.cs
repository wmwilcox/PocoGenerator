using Moq;
using NUnit.Framework;
using PocoGenerator.Common;
using System;
using System.IO;

namespace PocoGenerator.Tests.Common
{
	[TestFixture]
    public class FileUtilsTest
    {
		[Test]
        public void TestReadValuesFromTextFile() {
            string[] lines = new string[] { "one", "two", "three", "four", "five", null };
            int index = 0;
            var mockReader = new Mock<TextReader>();
            mockReader.Setup(x => x.ReadLine())
                .Returns(() => lines[index])
                .Callback(() => index++);

            var result = FileUtils.ReadValuesFromTextFile(mockReader.Object, new Func<string, string>(x => x));

            Assert.AreEqual(5, result.Count);
            for (int i = 0; i < 5; i++)
                Assert.AreEqual(lines[i], result[i]);
        }

        [Test]
        public void TestReadWeightedValuesFromTextFile() {
            string[] lines = new string[] { "one\t1", "two\t2", "three\t3", "four\t4", "five\t5", null };
            int index = 0;
            var mockReader = new Mock<TextReader>();
            mockReader.Setup(x => x.ReadLine())
                .Returns(() => lines[index])
                .Callback(() => index++);

            var result = FileUtils.ReadWeightedValuesFromTextFile(mockReader.Object, FileUtils.WeightedValueTSVLineParser);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(1, result["one"]);
            Assert.AreEqual(2, result["two"]);
            Assert.AreEqual(3, result["three"]);
            Assert.AreEqual(4, result["four"]);
            Assert.AreEqual(5, result["five"]);
             
        }
    }
}
