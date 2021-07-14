using System.Net.Http;
using POCJira.Shared.Configurations;

namespace POCJira.Repository.Repositories.Base
{
    public abstract class HttpRepositoryBase
    {
        public HttpClient httpClient { get; private set; }

        public HttpRepositoryBase(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);
        }
    }
}
