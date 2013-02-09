using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System;

namespace PocoGenerator.Tests.ValueGenerators.Compound
{
    [TestFixture]
    public class ConditionalValueGeneratorTest
    {
        private long idIfJohn = 6443L;
        private long idIfNotJohn = 783762L;

        [Test]
        public void TestConditionalValueGenerator_Literal_Values() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.Id).Use(Generators.LongSequence(0, 1))
                .For(x => x.FirstName).Use(Generators.Conditional<Person, String>()
                    .AddCondition(x => x.Id == 0L, "Zero")
                    .AddCondition(x => x.Id == 1L, "One")
                    .AddCondition(x => x.Id > 1L, "GreaterThanOne")
                );

            var result1 = generator.MakeOne();
            var result2 = generator.MakeOne();
            var result3 = generator.MakeOne();

            Assert.AreEqual("Zero", result1.FirstName);
            Assert.AreEqual("One", result2.FirstName);
            Assert.AreEqual("GreaterThanOne", result3.FirstName);
        }

        [Test]
        public void TestConditionalValueGenerator_RepeatableResultsWhenRandomSourceIsSupplied() {
            var rndSeed = 9;
            var rnd1 = new Random(rndSeed);
            var rnd2 = new Random(rndSeed);

            var gen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.Id).Use(Generators.LongSequence(0, 1))
                .For(x => x.FirstName).Use(Generators.Conditional<Person, String>()
                    .AddCondition(x => x.Id == 0L, "Zero")
                    .AddCondition(x => x.Id == 1L, "One")
                    .AddCondition(x => x.Id > 1L, Generators.FromLoremIpsum(20))
                );
            var gen2 = new PocoGenerator<Person>(rnd2)
                .For(x => x.Id).Use(Generators.LongSequence(0, 1))
                .For(x => x.FirstName).Use(Generators.Conditional<Person, String>()
                    .AddCondition(x => x.Id == 0L, "Zero")
                    .AddCondition(x => x.Id == 1L, "One")
                    .AddCondition(x => x.Id > 1L, Generators.FromLoremIpsum(20))
                );

            TestUtils.TestRandomResults(25, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.FirstName, b.FirstName);
            });
        }

        [Test]
        public void TestConditionalValueGenerator_IValue_Values() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.FirstName).Use("John")
                .For(x => x.Id).Use(Generators.Conditional<Person, long>()
                    .AddCondition(x => x.FirstName == "John", Generators.LongSequence(idIfJohn,1))
                    .AddCondition(x => x.FirstName != "John", idIfNotJohn)
                );

            var result = generator.MakeOne();

            Assert.AreEqual(idIfJohn, result.Id);
        }

        [Test]
        public void TestConditionalValueGenerator_No_Matches() {
            var generator = new PocoGenerator<Person>()
                .For(x => x.Id).Use(Generators.LongSequence(1000, 1))
                .For(x => x.FirstName).Use(Generators.Conditional<Person, String>()
                    .AddCondition(x => x.Id == 0L, "Zero")
                );

            var result = generator.MakeOne();

            Assert.AreEqual(default(string), result.FirstName);
        }

        [Test]
        public void TestConditionalValueGenerator_PredicatedOnUnBuiltProperty() {
            //NOTE: when using a ConditionalValueGenerator, the condition predicates
            //      are evaluated agains the current state of the object being built.
            //      The fields of this object are built in the order they are defined.
            //      Therefore, a ConditionalValueGenerator should proceed any other 
            //      IValueGenerator that it depends on.

            var generator = new PocoGenerator<Person>()
                .For(x => x.Id).Use(Generators.Conditional<Person, long>()
                    .AddCondition(x => x.FirstName == "John", Generators.LongSequence(idIfJohn, 1))
                    .AddCondition(x => x.FirstName != "John", idIfNotJohn))
                .For(x => x.FirstName).Use("John");

            var result = generator.MakeOne();

            Assert.AreEqual(idIfNotJohn, result.Id);
        }

        [Test]
        public void TestConditionalValueGenerator_PredicatedOnGeneratedCollectionAction() {
            //NOTE: when using WithGeneratedCollection, these actions are done *before* collections.
            //      this test ensures that these actions run before properties are set.

            long idIfAliasesIsEmpty = 3874343L;
            long idIfAliasesIsNotEmpty = 184733222L;

            var generator = new PocoGenerator<Person>()
                .For(x => x.Id).Use(Generators.Conditional<Person, long>()
                    .AddCondition(x => x.KnownAliases.Count == 0, Generators.LongSequence(idIfAliasesIsEmpty, 1))
                    .AddCondition(x => x.KnownAliases.Count > 0, idIfAliasesIsNotEmpty))
                .WithGeneratedCollection(1, Generators.FormattedString("name")).Do((p, a) => p.KnownAliases.Add(a));

            var result = generator.MakeOne();

            Assert.AreEqual(idIfAliasesIsNotEmpty, result.Id);
        }


    }
}
