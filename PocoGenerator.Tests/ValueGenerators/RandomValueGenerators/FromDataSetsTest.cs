using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System;

namespace PocoGenerator.Tests.ValueGenerators.RandomValueGenerators
{
    [TestFixture]
    public class FromDataSetsTest
    {
        private int rndSeed = 8166;
        private int makeCount = 10;

        private bool resourcesWereNotPrinted = true;

        [SetUp]
        public void setup() {
            if (resourcesWereNotPrinted)
            {
                //prints out all resources... for your reference
                Console.WriteLine("== All embedded resources in PocoGenerator ===");
                Type type = typeof(PocoGenerator.Generators);
                foreach (string resourceName in type.Assembly.GetManifestResourceNames())
                    Console.WriteLine("\t:: " + resourceName);
                resourcesWereNotPrinted = false;
            }
        }

        [Test]
        public void TestLoremIpsumGenerator_FixedSize() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.Bio).Use(Generators.FromLoremIpsum(50));

            var result = generator.Make(10);

            foreach (Person p in result)
                Assert.AreEqual(50, p.Bio.Length);
        }

        [Test]
        public void TestLoremIpsumGenerator_FromNormal() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.Bio).Use(Generators.FromLoremIpsum(Generators.FromNormal(50,10)));

            var result = generator.Make(10);

            Console.WriteLine("=== From Latin Words Dataset ===");
            foreach (Person p in result)
            {
                Assert.NotNull(p.Bio);
                Console.WriteLine("\t" + p.Bio);
            }
        }

        [Test]
        public void TestCityGenerator() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.City).Use(Generators.FromUSCities());

            var results = generator.Make(makeCount);
            Console.WriteLine("=== From USCity Dataset ===");
            foreach (var p in results)
            {
                Assert.NotNull(p.City);
                Console.WriteLine("\t:: " + p.City);
            }
        }

        [Test]
        public void TestCityGenerator_RepeatableResultsWhenRandomSourceIsSupplied() {
            var rnd1 = new Random(rndSeed);
            var rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.City).Use(Generators.FromUSCities());

            var gen2 = new PocoGenerator<Person>(rnd2)
                .For(x => x.City).Use(Generators.FromUSCities());

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.City, b.City);
            });
        }

        [Test]
        public void TestNameGenerators_RepeatableResultsWhenRandomSourceIsSupplied() {
            Random rnd1 = new Random(rndSeed);
            Random rnd2 = new Random(rndSeed);

            var maleGen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.FirstName).Use(Generators.FromMaleFirstNames())
                .For(x => x.LastName).Use(Generators.FromLastNames());
            var femaleGen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.FirstName).Use(Generators.FromFemaleFirstNames())
                .For(x => x.LastName).Use(Generators.FromLastNames());

            var maleGen2 = new PocoGenerator<Person>(rnd2)
                .For(x => x.FirstName).Use(Generators.FromMaleFirstNames())
                .For(x => x.LastName).Use(Generators.FromLastNames());
            var femaleGen2 = new PocoGenerator<Person>(rnd2)
                .For(x => x.FirstName).Use(Generators.FromFemaleFirstNames())
                .For(x => x.LastName).Use(Generators.FromLastNames());


            TestUtils.TestRandomResults(makeCount, maleGen1, maleGen2, (a, b) => {
                Assert.AreEqual(a.FirstName, b.FirstName);
                Assert.AreEqual(a.LastName, b.LastName);
            });

            TestUtils.TestRandomResults(makeCount, femaleGen1, femaleGen2, (a, b) => {
                Assert.AreEqual(a.FirstName, b.FirstName);
                Assert.AreEqual(a.LastName, b.LastName);
            });
        }

        [Test]
        public void TestNameGenerators() {

            var maleGenerator = new PocoGenerator<Person>()
                .For(x => x.FirstName).Use(Generators.FromMaleFirstNames())
                .For(x => x.LastName).Use(Generators.FromLastNames());

            var femaleGenerator = new PocoGenerator<Person>()
                .For(x => x.FirstName).Use(Generators.FromFemaleFirstNames())
                .For(x => x.LastName).Use(Generators.FromLastNames());

            var males = maleGenerator.Make(makeCount);
            var females = femaleGenerator.Make(makeCount);

            Console.WriteLine("=== From US Names Dataset ===");
            for (int i = 0; i < makeCount; i++)
            {
                var male = males[i];
                var female = females[i];
                Assert.NotNull(male.FirstName);
                Assert.NotNull(male.LastName);
                Assert.NotNull(female.FirstName);
                Assert.NotNull(female.LastName);
                Console.WriteLine("\t  male: " + male.FirstName + " " + male.LastName);
                Console.WriteLine("\tfemale: " + female.FirstName + " " + female.LastName);
            }

        }
    }
}
