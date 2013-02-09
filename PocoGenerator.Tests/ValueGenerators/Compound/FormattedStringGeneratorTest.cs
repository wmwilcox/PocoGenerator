using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using PocoGenerator.ValueGenerators.Literals;
using System;


namespace PocoGenerator.Tests.ValueGenerators.Compound
{
    [TestFixture]
    public class FormattedStringGeneratorTest
    {
        [Test]
        public void TestFormattedStringGenerator() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.PhoneNumber)
                .Use(Generators.FormattedString(
                    "({0}) {1}-{2}",
                        new LiteralValueGenerator<string>("555"),
                        new LiteralValueGenerator<int>(8675),
                        309));

            var result = generator.MakeOne();

            Assert.AreEqual("(555) 8675-309", result.PhoneNumber);
        }

        [Test]
        public void TestFormattedStringGenerator_RepeatableResultsWhenRandomSourceIsSupplied() {
            var rndSeed = 133;
            var rnd1 = new Random(rndSeed);
            var rnd2 = new Random(rndSeed);
            
            var gen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.PhoneNumber)
                .Use(Generators.FormattedString(
                    "({0}) {1}-{2}",
                        Generators.FromUniform(500,555),
                        Generators.FromUniform(500,555),
                        Generators.FromUniform(1000,9999)));

            var gen2 = new PocoGenerator<Person>(rnd2)
                .For(x => x.PhoneNumber)
                .Use(Generators.FormattedString(
                    "({0}) {1}-{2}",
                        Generators.FromUniform(500, 555),
                        Generators.FromUniform(500, 555),
                        Generators.FromUniform(1000, 9999)));

            TestUtils.TestRandomResults<Person>(25, gen1, gen2,
                (a, b) => { 
                    Assert.AreEqual(a.PhoneNumber, b.PhoneNumber);
                });
        }
    }
}
