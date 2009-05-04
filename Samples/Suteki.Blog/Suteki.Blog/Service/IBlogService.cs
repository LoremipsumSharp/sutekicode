using Suteki.Blog.Model;

namespace Suteki.Blog.Service
{
    public interface IBlogService
    {
        Post GetPost(int id);
        void AddPost(Post post);
    }
}