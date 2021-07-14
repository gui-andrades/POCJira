using System.Linq;
using System;
using System.Threading.Tasks;
using POCJira.Domain.Interfaces;
using POCJira.Repository.Interfaces;
using POCJira.Shared.Models;
using System.Collections.Generic;

namespace POCJira.Domain.Services
{
    public class UpdateSqlService : IUpdateSqlService
    {
        private readonly ICardsRepository cardsRepository;

        private readonly IJiraRepository jiraRepository;

        private static String AcceptedUpdate = "issue_generic";

        public UpdateSqlService(ICardsRepository cardsRepository, IJiraRepository jiraRepository)
        {
            this.cardsRepository = cardsRepository;
            this.jiraRepository = jiraRepository;
        }

        public async Task UpdateSqlCard(UpdateSqlModel input)
        {
            if (!input.Issue_Event_Type_Name.Equals(AcceptedUpdate))
            {
                return;
            }

            var validationErrors = ValidateChanges(input);

            if (validationErrors.Any())
            {
                await jiraRepository.RollbackJiraCard(input, validationErrors);
                return;
            }

            await cardsRepository.UpdateSqlCard(input);
        }

        private List<String> ValidateChanges(UpdateSqlModel input)
        {
            var newStatus = input.StatusChange.ToString;
            var validationErrors = new List<String>();

            switch (newStatus)
            {
                case "To Do":
                    break;
                case "In Progress":
                    ValidateInProgressChanges(input, validationErrors);
                    break;
                case "Done":
                    ValidateInProgressChanges(input, validationErrors);
                    ValidateDoneChanges(input, validationErrors);
                    break;
                default:
                    validationErrors.Add("Houve algum erro durante a validação. Verifique os campos e tente novamente.");
                    break;
            }

            return validationErrors;

        }

        private void ValidateInProgressChanges(UpdateSqlModel input, List<String> validationErrors)
        {
            if (input.Issue.Fields.Assignee == null)
            {
                validationErrors.Add("Um usuário deve ser marcado como responsável pelo chamado.");
            }
        }

        private void ValidateDoneChanges(UpdateSqlModel input, List<String> validationErrors)
        {
            if (input.Issue.Fields.CustomField_10030 == 0 || input.Issue.Fields.CustomField_10030 == null)
            {
                validationErrors.Add("Deve ser especificado o valor do chamado.");
            }
        }
    }
}
