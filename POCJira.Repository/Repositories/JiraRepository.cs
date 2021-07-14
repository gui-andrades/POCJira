using System.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using POCJira.Repository.Interfaces;
using POCJira.Repository.Repositories.Base;
using POCJira.Repository.Responses;
using POCJira.Shared.Models;
using System.Collections.Generic;

namespace POCJira.Repository.Repositories
{
    public class JiraRepository : HttpRepositoryBase, IJiraRepository
    {
        private readonly IMapper mapper;

        public JiraRepository(IHttpClientFactory httpClientFactory, IMapper mapper) : base(httpClientFactory)
        {
            this.mapper = mapper;
        }
        public async Task<JiraCardCreatedModel> CreateJiraCard(CreateJiraCardModel input)
        {
            var url = "issue";

            var postObject = new
            {
                fields = new
                {
                    project = new
                    {
                        id = 10000
                    },
                    issuetype = new
                    {
                        id = 10001
                    },
                    summary = input.Nome,
                    description = "Descrição: " + input.Descricao + "\nId: " + input.Id
                }
            };

            var response = await httpClient.PostAsJsonAsync(url, postObject);

            var stringResult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<JiraCardCreatedResponse>(stringResult);

            var resultModel = mapper.Map<JiraCardCreatedResponse, JiraCardCreatedModel>(result);

            return resultModel;
        }

        public async Task RollbackJiraCard(UpdateSqlModel input, List<String> validationErrors)
        {
            var transitionId = GetTransitionIdFromName(input.StatusChange.FromString);

            var comments = String.Join("\n", validationErrors);

            var putObject = new
            {
                update = new
                {
                    comment = new List<object>(){
                        new
                        {
                            add = new
                            {
                                body = comments
                            }
                        }
                    }
                }
            };

            var transitionTask = TransitionJiraCard(input.Issue.Id, transitionId);
            var fieldsTask = UpdateJiraCard(input.Issue.Id, putObject);

            var transitionResponse = await transitionTask;
            var fieldsResponse = await fieldsTask;
        }

        private async Task<HttpResponseMessage> UpdateJiraCard(String jiraCardId, object putObject)
        {
            var url = $"issue/{jiraCardId}";

            return await httpClient.PutAsJsonAsync(url, putObject);
        }

        private async Task<HttpResponseMessage> TransitionJiraCard(String jiraCardId, String transitionId)
        {

            var url = $"issue/{jiraCardId}/transitions";

            var transitionPostObject = new
            {
                transition = new
                {
                    id = transitionId
                }
            };

            return await httpClient.PostAsJsonAsync(url, transitionPostObject);
        }

        private String GetTransitionIdFromName(String transitionName)
        {
            return transitionName switch
            {
                "To Do" => "11",
                "In Progress" => "21",
                "Done" => "31",
                _ => String.Empty
            };
        }
    }
}
