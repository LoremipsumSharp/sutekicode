using System;
using Suteki.Blog.Model;

namespace Suteki.Blog.Tests.ModelTests.Builders
{
    public class PostBuilder : ModelBuilder<Post>
    {
        public string Title = "The Title";
        public string Text = "Some text for the post";
        public DateTime CreatedDate = new DateTime(2009, 5, 1);

        public override Post Build()
        {
            return new Post
            {
                Id = Id,
                Title = Title,
                Text = Text,
                CreatedDate = CreatedDate
            };
        }
    }
}