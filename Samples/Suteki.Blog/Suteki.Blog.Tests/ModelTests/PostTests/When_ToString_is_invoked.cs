using NUnit.Framework;

namespace Suteki.Blog.Tests.ModelTests.PostTests
{
    [TestFixture]
    public class When_ToString_is_invoked : PostTestBase
    {
        [Test]
        public void It_should_return_id_and_title()
        {
            var expectedText = string.Format("Post Id: {0} Title: '{1}'", post.Id, post.Title);
            Assert.That(post.ToString(), Is.EqualTo(expectedText));
        }
    }
}