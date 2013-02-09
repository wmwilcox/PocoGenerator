using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;

namespace PocoGenerator.Tests.ValueGenerators.Sequences
{
    [TestFixture]
    class SequenceValueGeneratorTest
    {
        int makeCount = 10;

        [Test]
        public void TestSequences_DefaultCtor() {
            var generator = new PocoGenerator<NumericValues>()
                .For(x => x.IntValue).Use(Generators.IntSequence())
                .For(x => x.UIntValue).Use(Generators.UIntSequence())
                .For(x => x.LongValue).Use(Generators.LongSequence())
                .For(x => x.ULongValue).Use(Generators.ULongSequence())
                .For(x => x.FloatValue).Use(Generators.FloatSequence())
                .For(x => x.DoubleValue).Use(Generators.DoubleSequence());

            var results = generator.Make(makeCount);

            for (int i = 0; i < makeCount; i++)
            {
                var curResult = results[i];
                Assert.AreEqual(i, curResult.IntValue);
                Assert.AreEqual(i, curResult.UIntValue);
                Assert.AreEqual((long)i, curResult.LongValue);
                Assert.AreEqual((ulong)i, curResult.ULongValue);
                Assert.AreEqual((float)i, curResult.FloatValue);
                Assert.AreEqual((double)i, curResult.DoubleValue);
            }
        }

        [Test]
        public void TestSequences_StartValueCtor() {
            var generator = new PocoGenerator<NumericValues>()
                .For(x => x.IntValue).Use(Generators.IntSequence(100))
                .For(x => x.UIntValue).Use(Generators.UIntSequence(100))
                .For(x => x.LongValue).Use(Generators.LongSequence(100L))
                .For(x => x.ULongValue).Use(Generators.ULongSequence(100L))
                .For(x => x.FloatValue).Use(Generators.FloatSequence(100.0F))
                .For(x => x.DoubleValue).Use(Generators.DoubleSequence(100.0D));

            var results = generator.Make(makeCount);

            for (int i = 0; i < makeCount; i++)
            {
                var curResult = results[i];
                Assert.AreEqual(i + 100, curResult.IntValue);
                Assert.AreEqual(i + 100, curResult.UIntValue);
                Assert.AreEqual((long)i + 100L, curResult.LongValue);
                Assert.AreEqual((ulong)i + 100L, curResult.ULongValue);
                Assert.AreEqual((float)i + 100F, curResult.FloatValue);
                Assert.AreEqual((double)i + 100D, curResult.DoubleValue);
            }
        }

        [Test]
        public void TestSequences_StartValueIncrementByCtor() {
            var generator = new PocoGenerator<NumericValues>()
                .For(x => x.IntValue).Use(Generators.IntSequence(100, 100))
                .For(x => x.UIntValue).Use(Generators.UIntSequence(100, 100))
                .For(x => x.LongValue).Use(Generators.LongSequence(100L ,100L))
                .For(x => x.ULongValue).Use(Generators.ULongSequence(100L, 100L))
                .For(x => x.FloatValue).Use(Generators.FloatSequence(100.0F, 100.0F))
                .For(x => x.DoubleValue).Use(Generators.DoubleSequence(100.0D, 100.0D));

            var results = generator.Make(makeCount);

            for (int i = 0; i < makeCount; i++)
            {
                var curResult = results[i];
                Assert.AreEqual((i*100) + 100, curResult.IntValue);
                Assert.AreEqual((i*100) + 100, curResult.UIntValue);
                Assert.AreEqual((long)(i*100) + 100L, curResult.LongValue);
                Assert.AreEqual((ulong)(i*100) + 100L, curResult.ULongValue);
                Assert.AreEqual((float)(i*100) + 100F, curResult.FloatValue);
                Assert.AreEqual((double)(i*100) + 100D, curResult.DoubleValue);
            }
        }
    }
}
