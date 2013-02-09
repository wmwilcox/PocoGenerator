using Moq;
using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System;

namespace PocoGenerator.Tests.ValueGenerators.RandomValueGenerators
{
    [TestFixture]
    public class DiscreteDistributionsTest
    {
        private int makeCount = 10;
        private int rndSeed = 111;

        [Test]
        public void TestIntFromUniform() {
            var generator = new PocoGenerator<NumericValues>()
                .For(x => x.IntValue).Use(Generators.FromUniform(10, 20));

            var results = generator.Make(makeCount);

            for (int i = 0; i < makeCount; i++)
                Assert.True(results[i].IntValue >= 10 && results[i].IntValue <= 20);
        }

        [Test]
        public void TestIntFromUniform_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<NumericValues>(rnd1)
                .For(x => x.IntValue).Use(Generators.FromUniform(10, 20));
            var gen2 = new PocoGenerator<NumericValues>(rnd2)
                .For(x => x.IntValue).Use(Generators.FromUniform(10, 20));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.IntValue, b.IntValue);
            });
        }

        [Test]
        public void TestLongFromUniform() {
            var generator = new PocoGenerator<NumericValues>()
                .For(x => x.LongValue).Use(Generators.FromUniform(10L, 20L));

            var results = generator.Make(makeCount);

            for (int i = 0; i < makeCount; i++)
                Assert.True(results[i].LongValue >= 10L && results[i].LongValue <= 20L);
        }

        [Test]
        public void TestLongFromUniform_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<NumericValues>(rnd1)
                .For(x => x.LongValue).Use(Generators.FromUniform(10L, 20L));
            var gen2 = new PocoGenerator<NumericValues>(rnd2)
                .For(x => x.LongValue).Use(Generators.FromUniform(10L, 20L));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) =>
            {
                Assert.AreEqual(a.LongValue, b.LongValue);
            });
        }

        [Test]
        public void TestIntFromNormal() {
            double[] draws = new double[] { 0.9, 0.9, 0.6, 0.11 };
            int index = 0;
            var mockRnd = new Mock<Random>();
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => draws[index])
                .Callback(() => index++);

            var generator = new PocoGenerator<NumericValues>(mockRnd.Object)
                .For(x => x.IntValue).Use(Generators.FromNormal(5, 2));

            var result = generator.MakeOne();

            Assert.AreEqual(5, result.IntValue);
        }

        [Test]
        public void TestLongFromNormal() {
            double[] draws = new double[] { 0.9, 0.9, 0.6, 0.11 };
            int index = 0;
            var mockRnd = new Mock<Random>();
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => draws[index])
                .Callback(() => index++);

            var generator = new PocoGenerator<NumericValues>(mockRnd.Object)
                .For(x => x.LongValue).Use(Generators.FromNormal(5L, 2L));

            var result = generator.MakeOne();

            Assert.AreEqual(5L, result.LongValue);
        }

        [Test]
        public void TestLongAndIntFromNormal_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<NumericValues>(rnd1)
                .For(x => x.LongValue).Use(Generators.FromNormal(10L, 5L))
                .For(x => x.IntValue).Use(Generators.FromNormal(10, 5));
            var gen2 = new PocoGenerator<NumericValues>(rnd2)
                .For(x => x.LongValue).Use(Generators.FromNormal(10L, 5L))
                .For(x => x.IntValue).Use(Generators.FromNormal(10, 5));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) =>
            {
                Assert.AreEqual(a.IntValue, b.IntValue);
                Assert.AreEqual(a.LongValue, b.LongValue);
            });
        }

        [Test]
        public void TestIntFromPareto() {
            double draw = 0.3386;
            var mockRnd = new Mock<Random>();
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => draw);

            var generator = new PocoGenerator<NumericValues>(mockRnd.Object)
                .For(x => x.IntValue).Use(Generators.IntFromPareto(1.0, 0.668));

            var result = generator.MakeOne();

            Assert.AreEqual(5, result.IntValue);
        }

        [Test]
        public void TestIntFromPareto_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<NumericValues>(rnd1)
                .For(x => x.IntValue).Use(Generators.IntFromPareto(1.0, 2.0));
            var gen2 = new PocoGenerator<NumericValues>(rnd2)
                .For(x => x.IntValue).Use(Generators.IntFromPareto(1.0, 2.0));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.IntValue, b.IntValue);
            });
        }

        [Test]
        public void TestLongFromPareto() {
            double draw = 0.3386;
            var mockRnd = new Mock<Random>();
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => draw);

            var generator = new PocoGenerator<NumericValues>(mockRnd.Object)
                .For(x => x.LongValue).Use(Generators.LongFromPareto(1.0, 0.668));

            var result = generator.MakeOne();

            Assert.AreEqual(5, result.LongValue);
        }


        [Test]
        public void TestLongFromPareto_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<NumericValues>(rnd1)
                .For(x => x.LongValue).Use(Generators.LongFromPareto(1.0, 2.0));
            var gen2 = new PocoGenerator<NumericValues>(rnd2)
                .For(x => x.LongValue).Use(Generators.LongFromPareto(1.0, 2.0));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.LongValue, b.LongValue);
            });
        }

    }
}
