using System.ServiceModel.Syndication;
using Suteki.Blog.Model;

namespace Suteki.Blog.RestService.Atom
{
    public interface IPostAtomFeedMapper
    {
        SyndicationFeed Map(Post[] posts);
    }
}