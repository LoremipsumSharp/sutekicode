using NUnit.Framework;
using Rhino.Mocks;
using Suteki.Blog.Model;

namespace Suteki.Blog.Tests.ServiceTests.IBlogServiceTests
{
    [TestFixture]
    public class When_AddPost_is_invoked : BlogServiceTestBase
    {
        private const int id = 44;
        private const string title = "This is the post title";

        protected override void DoSetup()
        {
            var post = new Post
            {
                Id = id,
                Title = title
            };

            blogService.AddPost(post);
        }

        [Test]
        public void It_should_write_a_log_message()
        {
            logger.AssertWasCalled(l => l.Log(string.Format("Post {0} added: '{1}'", id, title)));
        }
    }
}