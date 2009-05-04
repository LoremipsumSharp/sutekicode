using NUnit.Framework;
using Rhino.Mocks;
using Suteki.Blog.Client.Controller;
using Suteki.Blog.Service;

namespace Suteki.Blog.Tests.ControllerTests.BlogControllerTests
{
    public abstract class BlogControllerTestBase
    {
        protected BlogController blogController;
        protected IBlogService blogService;

        public abstract void DoSetup();

        [SetUp]
        public void SetUp() 
        {
            blogService = MockRepository.GenerateStub<IBlogService>();
            blogController = new BlogController(blogService);

            DoSetup();
        }
    }
}