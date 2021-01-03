using System;

namespace App
{
    [Example3.AutoDiagnostic]
    public static class Program
    {
        public static void Main()
        {
            Example1.HelloWorld.SayHello();
            Example2.HelloWorld.SayHello();
            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }
    }
}
