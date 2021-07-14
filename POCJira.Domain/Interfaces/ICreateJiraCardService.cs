using System;
using System.Threading.Tasks;
using POCJira.Shared.Models;

namespace POCJira.Domain.Interfaces
{
    public interface ICreateJiraCardService
    {
        Task<JiraCardCreatedModel> CreateJiraCard(CreateJiraCardModel input);
    }
}
