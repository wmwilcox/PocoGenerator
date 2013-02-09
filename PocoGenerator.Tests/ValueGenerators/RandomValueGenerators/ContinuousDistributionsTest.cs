using Moq;
using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using PocoGenerator.ValueGenerators.RandomValueGenerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PocoGenerator.Tests.ValueGenerators.RandomValueGenerators
{
    [TestFixture]
    public class ContinuousDistributionsTest
    {
        private int makeCount = 10;
        private int rndSeed = 111;

        [Test]
        public void TestFromUniform() {
            var generator = new PocoGenerator<NumericValues>()
                .For(x => x.DoubleValue).Use(Generators.FromUniform(10D, 20D));

            var results = generator.Make(makeCount);

            for (int i = 0; i < makeCount; i++)
                Assert.True(results[i].DoubleValue >= 10D && results[i].DoubleValue <= 20D);
        }

        [Test]
        public void TestFromUniform_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<NumericValues>(rnd1)
                .For(x => x.DoubleValue).Use(Generators.FromUniform(10D, 20D));
            var gen2 = new PocoGenerator<NumericValues>(rnd2)
                .For(x => x.DoubleValue).Use(Generators.FromUniform(10D, 20D));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.DoubleValue, b.DoubleValue);
            });
        }

        [Test]
        public void TestFromNormal() {
            double[] draws = new double[] { 0.9, 0.9, 0.6, 0.11 };
            int index = 0;
            var mockRnd = new Mock<Random>();
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => draws[index])
                .Callback(() => index++);

            var generator = new PocoGenerator<NumericValues>(mockRnd.Object)
                .For(x => x.DoubleValue).Use(Generators.FromNormal(5.0, 2.0));

            var result = generator.MakeOne();

            Assert.AreEqual(5.4624, result.DoubleValue, 0.00001);
        }

        [Test]
        public void TestFromNormal_Eyeball() {
            Console.WriteLine("=== Normal (10,2.5) ===");
            //No actual assertions here, just print distribution to make sure it "looks" like a normal
            double mean = 10.0;
            double stddev = 2.5;
            var rnd = new Random();
            var counts = new Dictionary<int, int>();
            for (int i = 0; i < 200; i++)
            {
                int result = (int)Math.Round(FromNormalGenerator.RandNormal(rnd, mean, stddev));
                if (counts.ContainsKey(result))
                    counts[result]++;
                else
                    counts.Add(result, 1);
            }

            var orderedResults = counts.Keys.OrderBy(x => x);
            foreach (var key in orderedResults)
            {
                if (key < 10)
                    Console.Write(" " + key + " : ");
                else
                    Console.Write(key + " : ");
                for (int i = 0; i < counts[key]; i++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }

        [Test]
        public void TestFromNormal_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<NumericValues>(rnd1)
                .For(x => x.DoubleValue).Use(Generators.FromNormal(10D, 5D));
            var gen2 = new PocoGenerator<NumericValues>(rnd2)
                .For(x => x.DoubleValue).Use(Generators.FromNormal(10D, 5D));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.DoubleValue, b.DoubleValue);
            });
        }

        [Test]
        public void TestFromPareto() {
            double draw = 0.3386;
            var mockRnd = new Mock<Random>();
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => draw);

            var generator = new PocoGenerator<NumericValues>(mockRnd.Object)
                .For(x => x.DoubleValue).Use(Generators.FromPareto(1.0, 0.668));

            var result = generator.MakeOne();

            Assert.AreEqual(5.05896234, result.DoubleValue, 0.00001);
        }

        [Test]
        public void TestFromPareto_ParameterChecking() {

            //Assertion for negative numbers...

            var exc = Assert.Throws<ArgumentOutOfRangeException>(() => Generators.FromPareto(-1.0, 0.3343));
            Assert.AreEqual("scale", exc.ParamName);
            Assert.True(exc.Message.Contains("scale must be a number greater than zero."));

            exc = Assert.Throws<ArgumentOutOfRangeException>(() => Generators.FromPareto(1.0, -0.3343));
            Assert.AreEqual("shape", exc.ParamName);
            Assert.True(exc.Message.Contains("shape must be a number greater than zero."));

            //Assertions for NAN
            exc = Assert.Throws<ArgumentOutOfRangeException>(() => Generators.FromPareto(Double.NaN, 0.3343));
            Assert.AreEqual("scale", exc.ParamName);
            Assert.True(exc.Message.Contains("scale must be a number greater than zero."));

            exc = Assert.Throws<ArgumentOutOfRangeException>(() => Generators.FromPareto(1.0, Double.NaN));
            Assert.AreEqual("shape", exc.ParamName);
            Assert.True(exc.Message.Contains("shape must be a number greater than zero."));
        }

        [Test]
        public void TestFromPareto_Eyeball() {
            Console.WriteLine("=== Pareto (1,1.029), x=[0,25] ===");
            //No actual assertions here, just print distribution to make sure it "looks" like a normal
            double scale = 1.0;
            double shape = 1.02943326;
            var rnd = new Random();
            var counts = new Dictionary<int, int>();
            for (int i = 0; i < 120; i++)
            {
                int result = (int)Math.Round(FromParetoGenerator.FromPareto(rnd, scale, shape));
                if (counts.ContainsKey(result))
                    counts[result]++;
                else
                    counts.Add(result, 1);
            }

            var max = counts.Keys.Max();

            for (int i = 0; i < 25; i++)
            {
                if (i < 10)
                    Console.Write(" " + i + " : ");
                else
                    Console.Write(i + " : ");
                if (counts.ContainsKey(i))
                {
                    for (int j = 0; j < counts[i]; j++)
                        Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        [Test]
        public void TestFromPareto_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<NumericValues>(rnd1)
                .For(x => x.DoubleValue).Use(Generators.FromPareto(1.0, 2.0));
            var gen2 = new PocoGenerator<NumericValues>(rnd2)
                .For(x => x.DoubleValue).Use(Generators.FromPareto(1.0, 2.0));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.DoubleValue, b.DoubleValue);
            });
        }
    }
}
