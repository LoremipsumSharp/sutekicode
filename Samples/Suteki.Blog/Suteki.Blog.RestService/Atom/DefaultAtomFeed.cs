using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using Suteki.Blog.Service;

namespace Suteki.Blog.RestService.Atom
{
    [ServiceContract]
    public interface IAtomFeed
    {
        [OperationContract]
        [WebGet(UriTemplate = "/")]
        Atom10FeedFormatter GetPosts();
    }

    public class DefaultAtomFeed : IAtomFeed
    {
        private readonly IBlogService blogService;
        private readonly IPostAtomFeedMapper postAtomFeedMapper;

        public DefaultAtomFeed(IBlogService blogService, IPostAtomFeedMapper postAtomFeedMapper)
        {
            this.blogService = blogService;
            this.postAtomFeedMapper = postAtomFeedMapper;
        }

        public Atom10FeedFormatter GetPosts()
        {
            var posts = blogService.GetPosts();
            return new Atom10FeedFormatter(postAtomFeedMapper.Map(posts));
        }
    }
}