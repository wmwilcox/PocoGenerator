using Moq;
using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System;

namespace PocoGenerator.Tests.ValueGenerators.RandomValueGenerators
{
    [TestFixture]
    public class DateGeneratorsTest
    {
        private int makeCount = 10;
        private int rndSeed = 93;

        [Test]
        public void TestDateFromUniform() {
            DateTime minDate = DateTime.Now;
            DateTime maxDate = minDate.AddYears(2);
            
            var generator = new PocoGenerator<Person>()
                .For(x => x.BirthDate).Use(Generators.FromUniform(minDate, maxDate));

            var results = generator.Make(makeCount);

            for (int i = 0; i < makeCount; i++)
            {
                Assert.True(results[i].BirthDate > minDate);
                Assert.True(results[i].BirthDate < maxDate);
            }
        }

        [Test]
        public void TestDateFromUniform_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.BirthDate).Use(Generators.FromUniform(DateTime.Now, DateTime.Now.AddYears(2)));
            var gen2 = new PocoGenerator<Person>(rnd2)
                .For(x => x.BirthDate).Use(Generators.FromUniform(DateTime.Now, DateTime.Now.AddYears(2)));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                //account for rounding...
                DateTime d1 = a.BirthDate;
                DateTime d2 = b.BirthDate;
                Assert.AreEqual(d1.Date, d2.Date);
                Assert.AreEqual(d1.Hour, d2.Hour);
                Assert.AreEqual(d1.Minute, d2.Minute);
                Assert.AreEqual(d1.Second, d2.Second);
            });
        }

        [Test]
        public void TestDateFromNormal() {
            DateTime meanDate = new DateTime(8734839L);
            TimeSpan stdDevTimespan = new TimeSpan(8722L);
            DateTime expectedDate = new DateTime(8736856L);

            double[] draws = new double[] { 0.9, 0.9, 0.6, 0.11 };
            int index = 0;
            var mockRnd = new Mock<Random>();
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => draws[index])
                .Callback(() => index++);

            var generator = new PocoGenerator<Person>(mockRnd.Object)
                .For(x => x.BirthDate).Use(Generators.FromNormal(meanDate, stdDevTimespan));

            var result = generator.MakeOne();

            Assert.AreEqual(expectedDate, result.BirthDate);
        }

        [Test]
        public void TestDateFromNormal_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.BirthDate).Use(Generators.FromNormal(DateTime.Now, TimeSpan.FromDays(2)));
            var gen2 = new PocoGenerator<Person>(rnd2)
                .For(x => x.BirthDate).Use(Generators.FromNormal(DateTime.Now, TimeSpan.FromDays(2)));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                //account for rounding...
                DateTime d1 = a.BirthDate;
                DateTime d2 = b.BirthDate;
                Assert.AreEqual(d1.Date, d2.Date);
                Assert.AreEqual(d1.Hour, d2.Hour);
                Assert.AreEqual(d1.Minute, d2.Minute);
                Assert.AreEqual(d1.Second, d2.Second);
            });
        }
    }
}
