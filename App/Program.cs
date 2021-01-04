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
            PrimeDisplayer();
            MovieDisplayer();
            Console.WriteLine("Press any key to exit !");
            Console.ReadKey();
        }

        private static void PrimeDisplayer()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var number = random.Next(1, 1_000_000);
            var isPrime = Example4.MathExtensions.IsPrime(number);
            Console.WriteLine($"{number} is Prime ? {isPrime}");
        }

        private static void MovieDisplayer()
        {
            var builder = new MovieBuilder();
            var releaseDate = new DateTime(2008, 1, 1);
            var movie = builder.Build("Taken", "BlaBla", releaseDate);
            Console.WriteLine($"Movie: {movie.Title}");
        }
    }
}
