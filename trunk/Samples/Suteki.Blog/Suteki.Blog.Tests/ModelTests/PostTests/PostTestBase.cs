using NUnit.Framework;
using Suteki.Blog.Model;
using Suteki.Blog.Tests.ModelTests.Builders;

namespace Suteki.Blog.Tests.ModelTests.PostTests
{
    public class PostTestBase
    {
        protected Post post;
        protected PostBuilder builder;

        [SetUp]
        public void SetUp()
        {
            builder = new PostBuilder();
            post = builder;
        }
    }
}