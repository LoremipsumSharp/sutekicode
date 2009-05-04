using System;

namespace Suteki.Blog.Model
{
    public class Post : ModelBase
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}