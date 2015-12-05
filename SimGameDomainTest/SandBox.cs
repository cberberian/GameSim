using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NUnit.Framework;
using SimGame.Domain;

namespace SimGameDomainTest
{
    [TestFixture]
    public class SandBox
    {
        [Test]
        public void test1()
        {
                        var string1 = "reverse this string of words";
                        var words = string1.Split(' ');
                        var ret = string.Join(" ",  words.Reverse());
                        Console.Write(ret);
            


            
        }

        [Test]
        public void Lambda1()
        {
            var pds = new ProductType[]
            {
                new ProductType
                {
                    Id = 2
                },
                new ProductType
                {
                    Id = 1
                },
                new ProductType
                {
                    Id = 4
                },
                new ProductType
                {
                    Id=3
                } 
            };

            //Left of x => x.Id
            var pe = Expression.Parameter(typeof (ProductType), "productType");
            //
            var exp = Expression.Lambda<Func<ProductType, int>>(Expression.Property(pe, "Id"), pe);

        }

        [Test]
        public void Substrings()
        {
            var words = "christopher berberian".Split(' ');
            var sb = new StringBuilder();
            foreach(var word in words)
                for (var startIndex = 0; startIndex < word.Length; startIndex++)
                {
                    for (var length = 2; length < ((word.Length + 1) - startIndex); length++)
                        sb.AppendLine(word.Substring(startIndex, length));
                }

            var result = sb.ToString();
            Console.Write(result);
        }

        [Test]
        public void EfficientSort()
        {
            int[] initial = { 1, 5, 3, 6, 2, 6};
            var sorted = new SortedSet<int>(initial);
            foreach (var i in sorted)
            {

            }

            var hashed = new HashSet<int>(initial);
            foreach (var i in hashed)
            {

            }
        }

    }
}