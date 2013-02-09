using NUnit.Framework;
using PocoGenerator.Common;
using PocoGenerator.Tests.TestPocos;
using System;
using System.Reflection;

namespace PocoGenerator.Tests.Common
{

    [TestFixture]
    class ReflectionUtilsTest
    {
        MemberInfo firstNameInfo = typeof(Person).GetMember("FirstName")[0];
        MemberInfo birthDateInfo = typeof(Person).GetMember("BirthDate")[0];
        MemberInfo getPrivateIdInfo = typeof(Person).GetMember("GetPrivateId")[0];
        MemberInfo someFieldBelongingToPersonInfo = typeof(Person).GetMember("someFieldBelongingToPerson")[0];

        Person person = new Person();

        [SetUp]
        public void setup() {
        }

        [Test]
        public void TestGetPropertyFromExpression() {
        
            var actualFirstNameInfo = 
                ReflectionUtils.GetPropertyFromExpression<Person, String>
                    (x => x.FirstName);
            var actualBirthDateInfo =
                ReflectionUtils.GetPropertyFromExpression<Person, DateTime>
                    (x => x.BirthDate);

            Assert.AreEqual(firstNameInfo, actualFirstNameInfo);
            Assert.AreEqual(birthDateInfo, actualBirthDateInfo);
        }

        [Test]
        public void TestGetPropertyFromExpression_FailsForNonMemberExpressions() {
            Assert.Throws(typeof(ArgumentException), delegate{
                ReflectionUtils.GetPropertyFromExpression<Person,long>
                    (x => x.GetPrivateId());
            });
        }

        [Test]
        public void TestSetMemberValue_SetProperties() {
            String expectedFirstName = "John";
            DateTime expectedBirthDate = DateTime.Now.AddDays(-10);

            ReflectionUtils.SetMemberValue
                (firstNameInfo, person, expectedFirstName);

            ReflectionUtils.SetMemberValue
                (birthDateInfo, person, expectedBirthDate);

            Assert.AreEqual(expectedBirthDate, person.BirthDate);
            Assert.AreEqual(expectedFirstName, person.FirstName);
        }

        [Test]
        public void TestSetMemberValue_SetFields() {
            String expectedValue = "someExpectedValue";

            ReflectionUtils.SetMemberValue(someFieldBelongingToPersonInfo, person, expectedValue);

            Assert.AreEqual(expectedValue, person.someFieldBelongingToPerson);
        }

        [Test]
        public void TestSetMemberValue_FailsIfNotPropertyOrMember() {
            Assert.Throws(typeof(ArgumentException), delegate {
                ReflectionUtils.SetMemberValue(getPrivateIdInfo, person, 123L);
            });
        }
    }
}
