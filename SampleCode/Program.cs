using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCode
{
    class Program
    {
        static void Main(string[] args)
        {App_Start.EntityFrameworkProfilerBootstrapper.PreStart();

//            var string1 = "reverse this string of words";
//            var words = string1.Split(' ');
//            var ret = string.Join(" ",  words.Reverse());
//            Console.Write(ret);
            Console.Read();

            var text = "welcome";
            var ind = Enumerable.Range(0, text.Length).Select(x => text.Substring(x));
            var result = String.Join("@", ind);
            Console.Write(result);
            var query =
                from i in Enumerable.Range(0, text.Length)
                from j in Enumerable.Range(0, text.Length - i + 1)
                where j >= 2
                select text.Substring(i, j);
            Console.Read();
        }
    }
}

