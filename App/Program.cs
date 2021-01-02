using System;
using HelloWorldGenerated;

namespace App
{
    public static class Program
    {
        public static void Main()
        {
            HelloWorld.SayHello();

            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }
    }
}
