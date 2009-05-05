using NUnit.Framework;
using Rhino.Mocks;
using Suteki.Blog.Model;

namespace Suteki.Blog.Tests.ControllerTests.BlogControllerTests
{
    [TestFixture]
    public class When_Get_is_invoked : BlogControllerTestBase
    {
        private const int postId = 7;
        private Post returnedPost;

        public override void DoSetup()
        {
            blogService.Stub(b => b.GetPost(postId.ToString())).Return(new Post
            {
                Id = postId
            });

            returnedPost = blogController.Get(postId.ToString());
        }

        [Test]
        public void BlogService_GetPost_should_be_called()
        {
            blogService.AssertWasCalled(b => b.GetPost(postId.ToString()));
        }

        [Test]
        public void BlogService_GetPost_should_return_the_correct_post()
        {
            Assert.That(returnedPost.Id, Is.EqualTo(postId));
        }
    }
}