using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace POCJira.Shared.Configurations
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly Application application;

        public AuthorizationHandler(Application application)
        {
            this.application = application;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(application.JiraApiKey))
            {
                request.Headers.Add("Authorization", application.JiraApiKey);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
