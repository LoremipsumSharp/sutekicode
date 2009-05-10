using System;
using System.Diagnostics;
using System.Web;
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

        public Post[] GetPosts()
        {
            logger.Log("Returned all posts");
            return new[]
            {
                GetPost("1"),
                GetPost("2"),
                GetPost("3")
            };
        }

        public Post GetPost(string id)
        {
            logger.Log(string.Format("Returned post with id {0}", id));
            
            Debug.WriteLine(string.Format("HttpContext.Current = {0}", 
                HttpContext.Current == null ? "null" : "not null"));

            return new Post
            {
                Id = GetPostId(id),
                CreatedDate = new DateTime(2009, 5, 1),
                Title = "My Interesting Post",
                Text = "Here is some interesting information"
            };

        }

        private static int GetPostId(string id)
        {
            int idAsInteger;
            if(int.TryParse(id, out idAsInteger))
            {
                return idAsInteger;
            }
            throw new ApplicationException(string.Format("id must be a valid integer value, but was: '{0}'", id));
        }

        public void AddPost(Post post)
        {
            logger.Log(string.Format("Post {0} added: '{1}'", post.Id, post.Title));
        }
    }
}