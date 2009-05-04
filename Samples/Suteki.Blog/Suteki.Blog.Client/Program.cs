using System;
using Castle.Windsor;
using Suteki.Blog.Client.IoC;
using Suteki.Blog.Model;

namespace Suteki.Blog.Client
{
    public class Program : IDisposable
    {
        private readonly IWindsorContainer container;

        static void Main()
        {
            using (var program = new Program())
            {
                program.Run();
            }

            Console.ReadLine();
        }

        public Program()
        {
            // container = ContainerBuilder.BuildForInProcess();
            container = ContainerBuilder.BuildForWebservice();
        }

        public void Dispose()
        {
            container.Dispose();
        }

        public void Run()
        {
            var post = new Post
            {
                Id = 19,
                Title = "The brand new post"
            };
            Request("Blog/Put", post);

            var postId = 7;
            Request("Blog/Get", postId);
        }

        /// <summary>
        /// Mimic a simple MVC Framework
        /// </summary>
        /// <param name="target">should have the format 'controller/action'</param>
        /// <param name="argument">any argument that the controller expects</param>
        public void Request(string target, object argument)
        {
            Console.WriteLine();
            Console.WriteLine("-- Request: {0} --", target);

            var requestItems = target.Split('/');
            if (requestItems.Length != 2) 
                throw new ApplicationException("malformed target, expected 'controller/action'");

            var controllerName = requestItems[0] + "Controller";
            var actionName = requestItems[1];

            var controller = container.Resolve(controllerName);

            var actionMethod = controller.GetType().GetMethod(actionName);

            var response = actionMethod.Invoke(controller, new [] { argument });
            WriteResponse(response);
        }

        public void WriteResponse(object response)
        {
            if(response == null) return;
            Console.WriteLine();
            Console.WriteLine("-- Response --");
            Console.WriteLine(response.ToString());
            Console.WriteLine();
        }
    }
}
