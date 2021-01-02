using System;

namespace App
{
    public static class Program
    {
        public static void Main()
        {
            Example1.HelloWorldGenerated.HelloWorld.SayHello();
            Example2.HelloWorldGenerated.HelloWorld.SayHello();
            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }
    }
}
