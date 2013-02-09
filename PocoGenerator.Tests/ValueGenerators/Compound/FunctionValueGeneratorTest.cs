using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System;
using System.Collections.Generic;

namespace PocoGenerator.Tests.ValueGenerators.Compound
{
    [TestFixture]
    public class FunctionValueGeneratorTest
    {
        [Test]
        public void TestFunctionValueGenerator_UsingGenerator() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.Id).Use(Generators.LongSequence(0, 1))
                .For(x => x.FirstName).Use(Generators.FromFunction<Person, String>(
                    (x) => { return "Name_" + x.Id.ToString(); }));

            List<Person> results = generator.Make(2);

            Assert.AreEqual("Name_0", results[0].FirstName);
            Assert.AreEqual("Name_1", results[1].FirstName);
        }

        [Test]
        public void TestFunctionValueGenerator_UsingFunc() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.Id).Use(Generators.LongSequence(0, 1))
                .For(x => x.FirstName).Use((x) => { return "Name_" + x.Id.ToString(); });

            List<Person> results = generator.Make(2);

            Assert.AreEqual("Name_0", results[0].FirstName);
            Assert.AreEqual("Name_1", results[1].FirstName);
        }
    }
}
