using System;

namespace Suteki.Blog.Model
{
    public class Post : ModelBase
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        public override string ToString()
        {
            return string.Format("Post Id: {0} Title: '{1}'", Id, Title);
        }
    }
}