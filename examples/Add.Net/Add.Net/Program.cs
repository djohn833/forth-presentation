using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
class Program
{
    public static int Add(int a, int b) => a + b;

    static void Main(string[] args)
    {
        Console.WriteLine("1 + 2 = {0}", Add(1, 2));
    }
}
