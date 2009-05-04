using Suteki.Blog.Model;

namespace Suteki.Blog.Tests.ModelTests.Builders
{
    public abstract class ModelBuilder<TModel> where TModel : class, IModel
    {
        public int Id = 0;

        public abstract TModel Build();

        public static implicit operator TModel(ModelBuilder<TModel> builder)
        {
            return builder.Build();
        }
    }
}