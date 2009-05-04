using NUnit.Framework;
using Rhino.Mocks;
using Suteki.Blog.Model;

namespace Suteki.Blog.Tests.ControllerTests.BlogControllerTests
{
    [TestFixture]
    public class When_Put_is_invoked : BlogControllerTestBase
    {
        protected Post post;

        public override void DoSetup()
        {
            post = new Post();
            blogController.Put(post);
        }

        [Test]
        public void AddPost_should_be_called_on_blogService()
        {
            blogService.AssertWasCalled(b => b.AddPost(post));
        }
    }
}