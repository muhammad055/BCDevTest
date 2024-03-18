public class HackerNewsService : IHackerNewsService
{
    private readonly HttpClient _httpClient;

    public HackerNewsService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<int>> GetBestStoriesIdsAsync(int n)
    {
        var response = await _httpClient.GetFromJsonAsync<int[]>("https://hacker-news.firebaseio.com/v0/beststories.json");
        return n == -1 ? response : response[..n];
    }

    public async Task<Story> GetStoryAsync(int storyId)
    {
        return await _httpClient.GetFromJsonAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
    }
}