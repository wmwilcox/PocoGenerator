using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocoGenerator.Tests.Examples
{
    public enum Gender {Male, Female};

    public class User
    {
        public long Id { get; set; }
        public Gender Gender {get; set;}
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Username { get; set; }
        public String PhoneNumber { get; set; }
        public List<Post> Posts { get; set; }

        public override string ToString() {
            var strbldr = new StringBuilder();
            strbldr.AppendLine("UserId: " + Id);
            strbldr.AppendLine("Gender: " + Gender);
            strbldr.AppendLine("Name: " + FirstName + " " + LastName);
            strbldr.AppendLine("Username: " + Username);
            strbldr.AppendLine("PhoneNumber: " + PhoneNumber);
            strbldr.AppendLine("Post Count: " + Posts.Count);
            foreach (Post p in Posts) 
                strbldr.AppendLine("\t" + p.ToString());
            strbldr.AppendLine("==============================");
            return strbldr.ToString();
        }
    }

    public class Post
    {
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public String Text { get; set; }

        public override string ToString() {
            var strbldr = new StringBuilder();
            strbldr.AppendLine("Post [" + Id + "] by author [" + AuthorId + "]");
            strbldr.AppendLine("\t\t" + Text);
            return strbldr.ToString();
        }
    }
}
