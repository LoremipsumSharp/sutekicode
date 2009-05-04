using NUnit.Framework;
using Suteki.Blog.Model;
using Suteki.Blog.Tests.ModelTests.Builders;

namespace Suteki.Blog.Tests.ModelTests.PostTests
{
    [TestFixture]
    public class When_Post_is_created
    {
        private Post post;
        private PostBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new PostBuilder();
            post = builder;
        }

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