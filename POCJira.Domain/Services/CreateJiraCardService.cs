using System;
using System.Threading.Tasks;
using POCJira.Domain.Interfaces;
using POCJira.Repository.Interfaces;
using POCJira.Shared.Models;

namespace POCJira.Domain.Services
{
    public class CreateJiraCardService : ICreateJiraCardService
    {
        private readonly IJiraRepository jiraRepository;

        public CreateJiraCardService(IJiraRepository jiraRepository)
        {
            this.jiraRepository = jiraRepository;
        }

        public async Task<JiraCardCreatedModel> CreateJiraCard(CreateJiraCardModel input)
        {
            return await jiraRepository.CreateJiraCard(input);
        }
    }
}
