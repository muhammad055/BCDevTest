public interface IHackerNewsService
{
    Task<IEnumerable<int>> GetBestStoriesIdsAsync(int n);
    Task<Story> GetStoryAsync(int storyId);
}