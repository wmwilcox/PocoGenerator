using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PocoGenerator.Tests.Examples
{
    [TestFixture]
    public class Examples
    {

        [Test]
        public void TestCreateBlogUsersWithPosts() {
            var generator = CreateUserGenerator();

            var results = generator.Make(3);

            foreach(User u in results)
                Console.WriteLine(u.ToString());

            foreach (User u in results) {
                Assert.True(u.PhoneNumber.StartsWith("(555)"));
                foreach(Post p in u.Posts)
                    Assert.AreEqual(p.AuthorId, u.Id);
            }
        }

        [Test, Timeout(1500), Description("This test is here to try and ensure changes do not accidently harm performace in a large way. When originally developed, this test took about 1300 ms on a 2.7 GHz machine.")]
        public void TestPerformanceBenchmark() {
            var timer = new Stopwatch();
            timer.Start();
            var generator = CreateUserGenerator();
            testTime(generator, new int[] { 8, 16, 32, 64, 128, 256, 512, 1024});
            timer.Stop();
            Console.WriteLine("Total time: " + timer.ElapsedMilliseconds + " ms.");
        }


        public static void testTime(PocoGenerator<User> generator, int[] times) {
            var timer = new Stopwatch();
            foreach (int x in times)
            {
                Console.Write("generating " + x + " units . . .  ");
                timer.Start();
                var results = generator.Make(x);
                timer.Stop();
                Console.WriteLine("Done in " + timer.ElapsedMilliseconds + " ms.");
                timer.Reset();
            }
        } 

        public PocoGenerator<User> CreateUserGenerator() {
            Random rnd = new Random(131604);

            var postGenerator = new PocoGenerator<Post>()
                .For(x => x.Id).Use(Generators.LongSequence(0, 1))
                .For(x => x.Text).Use(
                    Generators.FromLoremIpsum(Generators.FromNormal(40, 10)));

            return new PocoGenerator<User>(rnd)
                .For(x => x.Id).Use(Generators.LongSequence(0, 1))
                .For(x => x.Gender).Use(Generators.FromWeightedSelector(new WeightedSet<Gender>(){
                    {Gender.Male,   49},
                    {Gender.Female, 51}}))
                .For(x => x.FirstName).Use(Generators.Conditional<User, String>()
                    .AddCondition(u => u.Gender == Gender.Female, Generators.FromFemaleFirstNames())
                    .AddCondition(u => u.Gender == Gender.Male, Generators.FromMaleFirstNames()))
                .For(x => x.LastName).Use(Generators.FromLastNames())
                .For(x => x.Username).Use((u) => u.FirstName[0] + u.LastName)
                .For(x => x.PhoneNumber).Use(Generators.FormattedString(
                        "({0}) {1}-{2}",
                            "555",
                            Generators.FromUniform(100,999),
                            Generators.FromUniform(1000,9999)))
                .WithValues<Post>(Generators.IntFromPareto(1.0,0.5), postGenerator)
                    .Do((user, post) => user.Posts.Add(post))
                .WithInitializer(() => new User(){Posts = new List<Post>()})
                .WithFinalizer((u) =>
                {
                    foreach(Post p in u.Posts)
                        p.AuthorId = u.Id;
                });
        }
    }
}
