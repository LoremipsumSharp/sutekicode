using NUnit.Framework;
using Rhino.Mocks;
using Suteki.Blog.Model;

namespace Suteki.Blog.Tests.ServiceTests.IBlogServiceTests
{
    [TestFixture]
    public class When_GetPost_is_invoked : BlogServiceTestBase
    {
        private const int id = 2;
        private Post post;

        protected override void DoSetup()
        {
            post = blogService.GetPost(id.ToString());
        }

        [Test]
        public void It_should_return_a_non_null_post()
        {
            Assert.That(post, Is.Not.Null);
        }

        [Test]
        public void It_should_return_a_post_with_the_same_id()
        {
            Assert.That(post.Id, Is.EqualTo(id));
        }

        [Test]
        public void It_should_write_a_log_message()
        {
            logger.AssertWasCalled(l => l.Log(string.Format("Returned post with id {0}", id)));
        }
    }
}