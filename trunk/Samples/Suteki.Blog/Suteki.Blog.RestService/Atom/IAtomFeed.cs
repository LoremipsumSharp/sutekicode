using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;

namespace Suteki.Blog.RestService.Atom
{
    [ServiceContract]
    public interface IAtomFeed
    {
        [OperationContract]
        [WebGet(UriTemplate = "/")]
        Atom10FeedFormatter GetPosts();
    }
}