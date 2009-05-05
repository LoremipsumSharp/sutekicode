using NUnit.Framework;
using Rhino.Mocks;
using Suteki.Blog.Service;

namespace Suteki.Blog.Tests.ServiceTests.IBlogServiceTests
{
    [TestFixture]
    public abstract class BlogServiceTestBase
    {
        protected IBlogService blogService;
        protected ILogger logger;

        protected abstract void DoSetup();

        [SetUp]
        public void SetUp()
        {
            logger = MockRepository.GenerateStub<ILogger>();
            blogService = new DefaultBlogService(logger);

            DoSetup();
        }
    }
}