using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using PocoGenerator.ValueGenerators.Literals;
using PocoGenerator.ValueGenerators.RandomValueGenerators;
using System;
using System.Collections.Generic;


namespace PocoGenerator.Tests
{

    [TestFixture]
    public class PocoGeneratorTest
    {
        private string expectedFirstName = "John";
        private string expectedLastName = "Doe";


        private class Node
        {
            public int Id = 0;
            public Node NextNode = null;
        }

        [Test]
        public void TestThatSettingRandomSourceDoesNotCauseAnInfinteLoop() {
            /* This tests sets up a scenario where you want to generate
             * linked Lists of random lengths. It is a bit contrived, but
             * it tests that the propogation of RandomSource does not cause
             * an infinite loop.
             * 
             * The only real assertion this test does is assert that
             * it doesn't hang and never finish!
             * 
             */

            var nodeIdGenerator = Generators.IntSequence(0, 1);
            var childRndSource = new Random(12232);

            //The root generator first is only defined with a definition for its id.
            var rootNodeGenerator = new PocoGenerator<Node>()
                .For(x => x.Id).Use(Generators.IntSequence(0,1));

            //This generator defines id from rootNodeGenerator, but makes no 
            //definition for its NextNode, meaning it should be null.
            var nodeWithNoChildGenerator = new PocoGenerator<Node>()
                .CloneFrom(rootNodeGenerator);

            //This is the interesting node. It uses a conditional generator to define
            //it's next node.
            //The condition is that 50% of the time it will generate its NextNode as a node
            //w/ no children, and the other 50% of the time it will generator the root node.
            var nodeWithProbableChildGenerator = new PocoGenerator<Node>()
                .CloneFrom(rootNodeGenerator)
                .For(x => x.NextNode).Use(Generators.Conditional<Node, Node>()
                    .AddCondition((n) => childRndSource.Next() % 2 == 0, nodeWithNoChildGenerator)
                    .AddCondition((n) => true,                           rootNodeGenerator));

            //Now the magic happens. The root node now uses this for its NextNode generator.
            //At the moment that this generator is added to the root, the root will attempt
            //to pass along its RandomSource to all its children. And since its child
            //has a reference back to it, it could create an infinite loop if we
            //are not careful.
            rootNodeGenerator.For(x => x.NextNode).Use(nodeWithProbableChildGenerator);

            rootNodeGenerator.RandomSource = new Random(5);

            var result = rootNodeGenerator.Make(1);
        }

        [Test]
        public void TestSettingRandomProperty() {
            var makeCount = 25;
            var rnd1 = new Random(552);
            var rnd2 = new Random(552);
            var valGen1 = new Selector<string>(new List<string>() { "a", "b", "c", "d", "e", "f", "g" });
            var valGen2 = new Selector<string>(new List<string>() { "a", "b", "c", "d", "e", "f", "g" });

            var gen1 = new PocoGenerator<Person>(rnd1)
                .WithGeneratedCollection(1, valGen1).Do((person, name) => person.FirstName = name)
                .For(x => x.Id).Use(Generators.FromUniform(20L, 100L));
            var gen2 = new PocoGenerator<Person>()
                .WithGeneratedCollection(1, valGen2).Do((person, name) => person.FirstName = name)
                .For(x => x.Id).Use(Generators.FromUniform(20L, 100L));

            gen2.RandomSource = rnd2; //here this should trigger afterRandomSourceSet and propogate the RandomSource to all its children.

            var result1 = gen1.Make(makeCount);
            var result2 = gen2.Make(makeCount);

            for (int i = 0; i < makeCount; i++)
                Assert.AreEqual(result1[i].FirstName, result1[i].FirstName);
        }

        [Test]
        public void TestInitAndFinalize() {
            var generator = new PocoGenerator<Person>()
                .WithInitializer(() => new Person() { FirstName = expectedFirstName })
                .WithFinalizer((p) => p.LastName = expectedLastName);

            var result = generator.MakeOne();

            Assert.AreEqual(expectedFirstName, result.FirstName);
            Assert.AreEqual(expectedLastName, result.LastName);
        }

        [Test]
        public void TestCloneFrom_InitAndFinalize() {
            var parentGen = new PocoGenerator<Person>()
                .WithInitializer(() => new Person() { FirstName = expectedFirstName })
                .WithFinalizer((p) => p.LastName = expectedLastName);

            var childGen = new PocoGenerator<Person>()
                .CloneFrom(parentGen);

            var result = childGen.MakeOne();

            Assert.AreEqual(expectedFirstName, result.FirstName);
            Assert.AreEqual(expectedLastName, result.LastName);
        }

        [Test]
        public void TestCloneFrom_Properties() {
            var parentGen = new PocoGenerator<Person>()
                .For(x => x.FirstName).Use(expectedFirstName)
                .For(x => x.LastName).Use(expectedLastName);

            var childGen = new PocoGenerator<Person>()
                .CloneFrom(parentGen);

            var result = childGen.MakeOne();

            Assert.AreEqual(expectedFirstName, result.FirstName);
            Assert.AreEqual(expectedLastName, result.LastName);
        }

        [Test]
        public void TestCloneFrom_RandomSource() {
            var rnd = new Random(5);
            var expectedValue = 16;

            var parentGen = new PocoGenerator<Person>(rnd);

            var childGen = new PocoGenerator<Person>()
                .CloneFrom(parentGen);

            Assert.AreEqual(expectedValue, childGen.RandomSource.Next(50));
        }

        [Test]
        public void TestCloneFrom_CollectionDefinitions() {
            var valueGenerator = new LiteralValueGenerator<string>("a");

            var parentGen = new PocoGenerator<Person>()
                .WithGeneratedCollection(2, valueGenerator).Do((person, alias) => person.KnownAliases.Add(alias));

            var childGen = new PocoGenerator<Person>()
                .CloneFrom(parentGen);

            var result = childGen.MakeOne();

            Assert.AreEqual(2, result.KnownAliases.Count);
            Assert.AreEqual("a", result.KnownAliases[0]);
            Assert.AreEqual("a", result.KnownAliases[1]);
        }

    }
}
