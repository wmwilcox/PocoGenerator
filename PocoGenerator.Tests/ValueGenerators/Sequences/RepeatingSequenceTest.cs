using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System.Collections.Generic;

namespace PocoGenerator.Tests.ValueGenerators.Sequences
{
    [TestFixture]
    public class RepeatingSequenceTest
    {
        [Test]
        public void TestRepeatingSequence() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.FirstName).Use(Generators.RepeatableSequence(
                    new List<string>() { "one", "two", "three" }));

            List<Person> results = generator.Make(7);

            Assert.AreEqual("one", results[0].FirstName);
            Assert.AreEqual("two", results[1].FirstName);
            Assert.AreEqual("three", results[2].FirstName);
            Assert.AreEqual("one", results[3].FirstName);
            Assert.AreEqual("two", results[4].FirstName);
            Assert.AreEqual("three", results[5].FirstName);
            Assert.AreEqual("one", results[6].FirstName);
        }
    }
}
