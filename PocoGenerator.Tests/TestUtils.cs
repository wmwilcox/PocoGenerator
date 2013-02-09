using System;
using System.Collections.Generic;
using System.Threading;

namespace PocoGenerator.Tests
{
    public class TestUtils
    {

        public static void TestRandomResults<T>(int makeCount, 
            IValueGenerator<T> gen1, IValueGenerator<T> gen2, Action<T,T> callback) {

            var results1 = makeList(gen1, makeCount);
            //if RandomSource is not propogated, then the generators are using an instance
            //from new Random() - which is based on time, so we pause make sure they are 
            //different.
            Thread.Sleep(8);
            var results2 = makeList(gen2, makeCount);

            for (int i = 0; i < makeCount; i++)
                callback(results1[i], results2[i]);
        }

        private static List<T> makeList<T>(IValueGenerator<T> generator, int size) {
            var list = new List<T>();
            for (int i = 0; i < size; i++)
                list.Add(generator.MakeOne());
            return list;
        }
    
    }
}
