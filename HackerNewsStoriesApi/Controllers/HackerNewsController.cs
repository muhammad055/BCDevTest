using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;

public class HackerNewsController : ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;
    private readonly IMemoryCache _cache;

    public HackerNewsController(IHackerNewsService hackerNewsService,IMemoryCache cache)
    {
        _hackerNewsService = hackerNewsService;
        _cache = cache;
    }
    /// <summary>
    /// It returns n best stories among all the stories sorted based on scores
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    [HttpGet("bestbyscore")]
    public async Task<ActionResult<IEnumerable<Story>>> GetBestStories(int n)
    {
        var cacheKey = $"BestStoryIds";
        var cacheStoryKey ="Stories";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<int> Ids)|| 
        !_cache.TryGetValue(cacheStoryKey, out IEnumerable<Story> cacheStories))
        {
        var storyIds = await _hackerNewsService.GetBestStoriesIdsAsync(-1);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                
            _cache.Set(cacheKey, storyIds, cacheEntryOptions);

        var tasks = storyIds.Select(id => _hackerNewsService.GetStoryAsync(id));
        var stories = await Task.WhenAll(tasks);
        
        _cache.Set(cacheStoryKey,stories.OrderByDescending(s => s.Score),cacheEntryOptions);
        return Ok(_cache.Get<IEnumerable<Story>>(cacheStoryKey).Take(n));
        
        }
         return Ok(cacheStories.Take(n));
    }
     
     /// <summary>
     /// It returns n best stories sorted based on scores but it just selects the Ids returned from API and then sort it
     /// not considering the actual best score among all the stories
     /// </summary>
     /// <param name="n">number of stories to return </param>
     /// <returns></returns>
     [HttpGet("best")]
    public async Task<ActionResult<IEnumerable<Story>>> GetBestStoriesByScore(int n)
    {
        var storyIds = await _hackerNewsService.GetBestStoriesIdsAsync(n);
        var tasks = storyIds.Select(id => _hackerNewsService.GetStoryAsync(id));
        var stories = await Task.WhenAll(tasks);
        return Ok(stories.OrderByDescending(s => s.Score));
    }
}