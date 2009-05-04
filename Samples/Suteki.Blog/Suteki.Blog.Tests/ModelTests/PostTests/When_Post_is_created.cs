using NUnit.Framework;

namespace Suteki.Blog.Tests.ModelTests.PostTests
{
    [TestFixture]
    public class When_Post_is_created : PostTestBase
    {
        [Test]
        public void It_should_not_be_null()
        {
            Assert.That(post, Is.Not.Null);
        }

        [Test]
        public void It_should_have_an_id()
        {
            Assert.That(post.Id, Is.EqualTo(builder.Id));
        }

        [Test]
        public void It_should_have_a_title()
        {
            Assert.That(post.Title, Is.EqualTo(builder.Title));
        }

        [Test]
        public void It_should_have_text()
        {
            Assert.That(post.Text, Is.EqualTo(builder.Text));
        }

        [Test]
        public void It_should_have_a_created_date()
        {
            Assert.That(post.CreatedDate, Is.EqualTo(builder.CreatedDate));
        }
    }
}