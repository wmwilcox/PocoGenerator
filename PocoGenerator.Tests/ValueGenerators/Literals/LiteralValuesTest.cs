using NUnit.Framework;
using PocoGenerator.Tests.TestPocos;
using System;
using System.Collections.Generic;

namespace PocoGenerator.Tests.ValueGenerators.Literals
{

    [TestFixture]
    class LiteralValuesTest
    {
        private DateTime birthDate = DateTime.Now.AddYears(-21);
        private PocoGenerator<Person> personGenerator;

        [SetUp]
        public void setup() {
            personGenerator = new PocoGenerator<Person>()
                .For(x => x.FirstName).Use("John")
                .For(x => x.LastName).Use("Doe")
                .For(x => x.BirthDate).Use(birthDate)
                .For(x => x.Id).Use(100L);
        }

        [Test]
        public void TestLiteralValues_MakeOne() {
            Person person = personGenerator.MakeOne();

            Assert.AreEqual("John", person.FirstName);
            Assert.AreEqual("Doe", person.LastName);
            Assert.AreEqual(birthDate, person.BirthDate);
            Assert.AreEqual(100L, person.Id);
        }

        [Test]
        public void TestLiteralValues_MakeN() {
            List<Person> people = personGenerator.Make(5);

            Assert.AreEqual(5, people.Count);
            foreach (Person person in people)
            {
                Assert.AreEqual("John", person.FirstName);
                Assert.AreEqual("Doe", person.LastName);
                Assert.AreEqual(birthDate, person.BirthDate);
                Assert.AreEqual(100L, person.Id);
            }
        }
    }
}
