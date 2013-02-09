using System;
using System.Collections.Generic;

namespace PocoGenerator.Tests.TestPocos
{
    public enum PersonType
    {
        Infant, Child, YoungAdult, Adult, Senior
    }

    public class Person
    {
        public Person() {
            KnownAliases = new List<string>();
            AliasesById = new Dictionary<int, string>();
        }

        public List<String> KnownAliases;
        public Dictionary<int, String> AliasesById;

        public String someFieldBelongingToPerson = String.Empty;

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public String PhoneNumber { get; set; }

        public String City { get; set; }

        public String Bio { get; set; }

        public long Id { get; set; }

        private long privateId = -123L;

        public long GetPrivateId() {
            return privateId;
        }
    }
}
