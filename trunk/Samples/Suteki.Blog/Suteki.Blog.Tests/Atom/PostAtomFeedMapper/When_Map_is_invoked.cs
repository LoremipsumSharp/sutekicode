using System;
using System.Linq;
using System.ServiceModel.Syndication;
using NUnit.Framework;
using Suteki.Blog.Model;
using Suteki.Blog.RestService.Atom;

namespace Suteki.Blog.Tests.Atom.PostAtomFeedMapper
{
    [TestFixture]
    public class When_Map_is_invoked
    {
        private IPostAtomFeedMapper postAtomFeedMapper;
        private Post[] posts;
        private SyndicationFeed syndicationFeed;

        [SetUp]
        public void SetUp()
        {
            postAtomFeedMapper = new DefaultPostAtomFeedMapper();

            posts = new[]
            {
                new Post
                {
                    Id = 1,
                    CreatedDate = new DateTime(2009, 5, 4),
                    Text = "Some text for the first post",
                    Title = "The title of the first post"
                },
                new Post
                {
                    Id = 2,
                    CreatedDate = new DateTime(2009, 5, 7),
                    Text = "Some text for the second post",
                    Title = "The title of the second post"
                }
            };

            syndicationFeed = postAtomFeedMapper.Map(posts);
        }

        [Test]
        public void The_syndicationFeed_should_contain_all_the_posts()
        {
            Assert.That(syndicationFeed.Items.Count(), Is.EqualTo(2));
            CompareItemToPost(syndicationFeed.Items.First(), posts[0]);
            CompareItemToPost(syndicationFeed.Items.Last(), posts[1]);
        }

        private static void CompareItemToPost(SyndicationItem item, Post post)
        {
            Assert.That(item.Id, Is.EqualTo(post.Id.ToString()));
            Assert.That(item.Title.Text, Is.EqualTo(post.Title));
            Assert.That(item.PublishDate.Date, Is.EqualTo(post.CreatedDate));
        }
    }
}