namespace Piaskownica.Components.SimplePage
{
    public class SimplePageContext
    {
        public event Action? OnArticleAdded;
        public event Action? OnHeadingAdded;

        public List<SimplePageArticle> Articles { get; } = [];
        public List<SimplePageArticleHeading> Headings { get; } = [];

        public Task AddArticle(SimplePageArticle article)
        {
            Articles.Add(article);
            OnArticleAdded?.Invoke();
            return Task.CompletedTask;
        }
        public Task AddHeading(SimplePageArticleHeading heading)
        {
            Headings.Add(heading);
            OnHeadingAdded?.Invoke();
            return Task.CompletedTask;
        }
    }
}
