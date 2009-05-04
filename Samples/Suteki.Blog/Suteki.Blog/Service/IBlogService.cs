using System.ServiceModel;
using Suteki.Blog.Model;

namespace Suteki.Blog.Service
{
    [ServiceContract(Namespace = "http://suteki.co.uk/blogService")]
    public interface IBlogService
    {
        [OperationContract]
        Post GetPost(int id);

        [OperationContract]
        void AddPost(Post post);
    }
}