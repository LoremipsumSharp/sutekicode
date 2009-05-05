using System;
using Suteki.Blog.ConsoleService.IoC;

namespace Suteki.Blog.ConsoleService
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Starting Service, hit Enter to close");

            using(ContainerBuilder.Build())
            {
                Console.ReadLine();
            }
        }
    }
}
