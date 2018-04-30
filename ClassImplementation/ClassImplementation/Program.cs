using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Driver d = new Driver("word");
            d.testFunctions();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
