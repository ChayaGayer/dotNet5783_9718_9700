//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;

namespace Stage0
{
    partial class program
    {
        static void Main(String[]args)
        {
            welcome9718();
            welcome9700();
            Console.ReadKey();
        }
        static partial void welcome9700();
        private static void welcome9718()
        {
            string x;
            Console.Write("Enter your name: ");
            x = Console.ReadLine();
            Console.WriteLine("{0} welcome to my first console application", x);
        }
    }
}