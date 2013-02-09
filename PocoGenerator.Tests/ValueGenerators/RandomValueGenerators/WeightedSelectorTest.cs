using Moq;
using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System;

namespace PocoGenerator.Tests.ValueGenerators.RandomValueGenerators
{
    [TestFixture]
    class WeightedSelectorTest
    {
        private int makeCount = 10;

        [Test]
        public void TestWeightedSelector_UniformDistribution() {
            var mockRnd = new Mock<Random>();
            var callback = 0.05d;
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => callback)
                .Callback(() => callback += 0.20d);
            
            var generator = new PocoGenerator<Person>(mockRnd.Object)
                .For(x => x.FirstName).Use(Generators.FromWeightedSelector(
                    new WeightedSet<string>(){
                        {"a",1},
                        {"b",1},
                        {"c",1},
                        {"d",1},
                        {"e",1}
                    }
                ));

           
            Assert.AreEqual("a", generator.MakeOne().FirstName); //0.05
            Assert.AreEqual("b", generator.MakeOne().FirstName); //0.25
            Assert.AreEqual("c", generator.MakeOne().FirstName); //0.45
            Assert.AreEqual("d", generator.MakeOne().FirstName); //0.65
            Assert.AreEqual("e", generator.MakeOne().FirstName); //0.85
        }

        [Test]
        public void TestWeightedSelector_UniformDistribution_EdgeCase() {
            var mockRnd = new Mock<Random>();
            var callback = 0.20d;
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => callback)
                .Callback(() => callback += 0.20d);

            var generator = new PocoGenerator<Person>(mockRnd.Object)
                .For(x => x.FirstName).Use(Generators.FromWeightedSelector(
                    new WeightedSet<string>(){
                        {"a",1},
                        {"b",1},
                        {"c",1},
                        {"d",1},
                        {"e",1}
                    }
                ));

            Assert.AreEqual("a", generator.MakeOne().FirstName); //0.2
            Assert.AreEqual("b", generator.MakeOne().FirstName); //0.4
            Assert.AreEqual("c", generator.MakeOne().FirstName); //0.6
            Assert.AreEqual("d", generator.MakeOne().FirstName); //0.8
            Assert.AreEqual("e", generator.MakeOne().FirstName); //1.0
        }

        [Test]
        public void TestWeightedSelector_NonUniformDistribution() {
            var mockRnd = new Mock<Random>();
            int selectionIndex = 0;
            double[] callbacks = new double[]{0.05d, 0.29d, 0.59d, 0.87d, 0.99d};
            mockRnd.Setup(x => x.NextDouble())
                .Returns(() => callbacks[selectionIndex])
                .Callback(() => selectionIndex++);

            var generator = new PocoGenerator<Person>(mockRnd.Object)
                .For(x => x.FirstName).Use(Generators.FromWeightedSelector(
                    new WeightedSet<string>(){
                        {"a",10},
                        {"b",20},
                        {"c",50},
                        {"d",10},
                        {"e",10}
                    }
                ));

            Assert.AreEqual("a", generator.MakeOne().FirstName); //0.05
            Assert.AreEqual("b", generator.MakeOne().FirstName); //0.29
            Assert.AreEqual("c", generator.MakeOne().FirstName); //0.59
            Assert.AreEqual("d", generator.MakeOne().FirstName); //0.87
            Assert.AreEqual("e", generator.MakeOne().FirstName); //0.99
        }

        [Test]
        public void TestWeightedSelector_RepeatableResultsWhenRandomSourceIsSupplied() {
            int mySeed = 712820;
            var rnd1 = new Random(mySeed);
            var rnd2 = new Random(mySeed);
            var gen1 = new PocoGenerator<Person>(rnd1)
                .For(x => x.FirstName).Use(Generators.FromWeightedSelector(
                    new WeightedSet<string>(){
                                    {"a",10},
                                    {"b",20},
                                    {"c",50},
                                    {"d",10},
                                    {"e",10}
                                }
                ));
            var gen2 = new PocoGenerator<Person>(rnd2)
             .For(x => x.FirstName).Use(Generators.FromWeightedSelector(
                 new WeightedSet<string>(){
                                            {"a",10},
                                            {"b",20},
                                            {"c",50},
                                            {"d",10},
                                            {"e",10}
                                        }
             ));

            TestUtils.TestRandomResults(makeCount, gen1, gen2, (a, b) => {
                Assert.AreEqual(a.FirstName, b.FirstName);
            });
        }
    }
}
