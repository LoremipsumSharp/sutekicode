using System.Collections.Generic;
using System.ServiceModel.Syndication;
using Suteki.Blog.Model;

namespace Suteki.Blog.RestService.Atom
{
    public class DefaultPostAtomFeedMapper : IPostAtomFeedMapper
    {
        public SyndicationFeed Map(Post[] posts)
        {
            var items = new List<SyndicationItem>();

            foreach (var post in posts)
            {
                var item = new SyndicationItem
                {
                    Id = post.Id.ToString(),
                    Title = new TextSyndicationContent(post.Title),
                    Content = new TextSyndicationContent(post.Text),
                    PublishDate = post.CreatedDate
                };
                items.Add(item);
            }

            return new SyndicationFeed(items);
        }
    }
}