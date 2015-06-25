using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaybeMonad
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
        }

        Program()
        {
            var a = from x in "1".ParseInt()
                    from y in "2".ParseInt()
                    from z in "3".ParseInt()
                    // コメントアウトするとa.IsNoneがtrueになる
                    // from e in "あ".ParseInt()
                    select x + y + z;

            if (a.IsNone)
            {
                Console.WriteLine("失敗");
                return;
            }

            Console.WriteLine(a.Value);
        }
    }

    static class StringEx
    {
        public static Option<int> ParseInt(this string s)
        {
            int v;
            if (int.TryParse(s, out v))
            {
                return Option<int>.Just(v);
            }

            return Option<int>.None();
        }
    }
}

