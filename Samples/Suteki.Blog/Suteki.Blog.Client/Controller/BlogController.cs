using System;
using Suteki.Blog.Model;
using Suteki.Blog.Service;

namespace Suteki.Blog.Client.Controller
{
    public class BlogController : IController
    {
        private readonly IBlogService service;

        public BlogController(IBlogService service)
        {
            this.service = service;
        }

        public void Put(Post post)
        {
            service.AddPost(post);
        }

        public Post Get(string postId)
        {
            return service.GetPost(postId);
        }
    }
}