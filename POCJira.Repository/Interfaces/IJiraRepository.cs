using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POCJira.Shared.Models;

namespace POCJira.Repository.Interfaces
{
    public interface IJiraRepository
    {
        Task<JiraCardCreatedModel> CreateJiraCard(CreateJiraCardModel input);

        Task RollbackJiraCard(UpdateSqlModel input, List<String> validationErrors);
    }
}
