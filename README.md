# Hacker News API Service

This project implements a RESTful API using ASP.NET Core to retrieve the best stories from the Hacker News API. It provides two endpoints: one to fetch the best stories sorted by their scores among all stories, and the other to fetch the best stories sorted by their scores from the IDs returned by the Hacker News API.

## Endpoints
### Get Best Stories By Score
#### GET /api/hackernews/bestbyscore?n={number}
Returns the top n best stories among all stories, sorted based on their scores.

### Get Best Stories
#### GET /api/hackernews/best?n={number}
Returns the top n best stories, sorted based on their scores. It selects the IDs returned from the Hacker News API and then sorts them, not considering the actual best score among all the stories.

## Caching

This project utilizes in-memory caching to store fetched stories and their IDs for improved performance. For microservice architecture, Redis cache could be used to scale the caching mechanism.

## Rate Limiting

The API implements rate limiting to control the number of requests made to the service. This helps prevent overloading the Hacker News API and ensures fair usage of resources.

## Installation and Usage
1. Clone the repository:


```bash
git clone https://github.com/your-username/hacker-news-api.git
cd hacker-news-api
```

2. Build the project:
```bash
dotnet build
```
3. Run the project:
```bash
dotnet run
```
The API will be accessible at http://localhost:{port}/api/hackernews/.


## Dependencies
- ASP.NET Core
- Newtonsoft.Json (for JSON serialization)
- AspNetCoreRateLimit (for rate limiting)
- Microsoft.Extensions.Caching.Memory (for in-memory caching)


## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
