using System.ServiceModel;
using System.ServiceModel.Web;
using Suteki.Blog.Model;

namespace Suteki.Blog.Service
{
    [ServiceContract(Namespace = "http://suteki.co.uk/blogService")]
    public interface IBlogService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/")]
        Post[] GetPosts();

        [OperationContract]
        [WebGet(UriTemplate = "/{id}")]
        Post GetPost(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/", Method = "POST")]
        void AddPost(Post post);
    }
}