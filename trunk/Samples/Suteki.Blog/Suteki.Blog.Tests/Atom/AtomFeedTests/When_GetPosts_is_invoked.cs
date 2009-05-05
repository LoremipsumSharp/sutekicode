using System.ServiceModel.Syndication;
using NUnit.Framework;
using Rhino.Mocks;
using Suteki.Blog.Model;
using Suteki.Blog.RestService.Atom;
using Suteki.Blog.Service;

namespace Suteki.Blog.Tests.Atom.AtomFeedTests
{
    [TestFixture]
    public class When_GetPosts_is_invoked
    {
        private IAtomFeed atomFeed;
        private IBlogService blogService;
        private IPostAtomFeedMapper postAtomFeedMapper;

        private Post[] posts;
        private SyndicationFeed syndicationFeed;
        private Atom10FeedFormatter atom10FeedFormatter;

        [SetUp]
        public void SetUp()
        {
            blogService = MockRepository.GenerateStub<IBlogService>();
            posts = new Post[0];
            blogService.Stub(s => s.GetPosts()).Return(posts);

            postAtomFeedMapper = MockRepository.GenerateStub<IPostAtomFeedMapper>();
            syndicationFeed = new SyndicationFeed();
            postAtomFeedMapper.Stub(m => m.Map(posts)).Return(syndicationFeed);

            atomFeed = new DefaultAtomFeed(blogService, postAtomFeedMapper);

            atom10FeedFormatter = atomFeed.GetPosts();
        }

        [Test]
        public void Atom10FeedFormatter_should_contain_the_syndication_feed()
        {
            Assert.That(atom10FeedFormatter.Feed, Is.SameAs(syndicationFeed));
        }
    }
}