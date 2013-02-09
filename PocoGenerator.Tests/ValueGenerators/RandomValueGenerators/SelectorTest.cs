using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System;
using System.Collections.Generic;

namespace PocoGenerator.Tests.ValueGenerators.RandomValueGenerators
{
    [TestFixture]
    class SelectorTest
    {
        private int makeCount = 10;

        private List<String> firstNames = new List<String> { "a", "b", "c", "d", "e" };
        private HashSet<String> firstNamesSet = new HashSet<string>();
        private HashSet<long> ids = new HashSet<long> { 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L, 9L, 10L };

        [SetUp]
        public void Setup() {
            firstNamesSet = new HashSet<string>(firstNames);
        }

        [Test]
        public void TestSelector_FromList() {
            PocoGenerator<Person> generator = new PocoGenerator<Person>()
                .For(x => x.FirstName).Use(Generators.FromSelector<String>(firstNames));
            List<Person> result = generator.Make(makeCount);

            foreach (Person person in result)
                Assert.True(firstNamesSet.Contains(person.FirstName));
        }

        [Test]
        public void TestSelector_FromSet() {
            PocoGenerator<Person> generator = new PocoGenerator<Person>()
                .For(x => x.Id).Use(Generators.FromSelector<long>(ids));
            List<Person> result = generator.Make(makeCount);

            foreach (Person person in result)
                Assert.True(ids.Contains(person.Id));
        }

        [Test]
        public void TestSelector_RepeatableResultsWhenRandomSourceIsSupplied() {
            int mySeed = 712820;
            PocoGenerator<Person> gen1 = new PocoGenerator<Person>(new Random(mySeed))
                .For(x => x.FirstName).Use(Generators.FromSelector<String>(firstNames))
                .For(x => x.Id).Use(Generators.FromSelector<long>(ids));
            PocoGenerator<Person> gen2 = new PocoGenerator<Person>(new Random(mySeed))
                .For(x => x.FirstName).Use(Generators.FromSelector<String>(firstNames))
                .For(x => x.Id).Use(Generators.FromSelector<long>(ids));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.Id, b.Id);
                Assert.AreEqual(a.FirstName, b.FirstName);
            });
        }
    }
}
