using System;
using Suteki.Blog.Model;

namespace Suteki.Blog.Service
{
    public class DefaultBlogService : IBlogService
    {
        private readonly ILogger logger;

        public DefaultBlogService(ILogger logger)
        {
            this.logger = logger;
        }

        public Post GetPost(int id)
        {
            logger.Log(string.Format("Returned post with is {0}", id));

            return new Post
            {
                Id = id,
                CreatedDate = new DateTime(2009, 5, 1),
                Title = "My Interesting Post",
                Text = "Here is some interesting information"
            };
        }

        public void AddPost(Post post)
        {
            logger.Log(string.Format("Post {0} added: '{1}'", post.Id, post.Title));
        }
    }
}