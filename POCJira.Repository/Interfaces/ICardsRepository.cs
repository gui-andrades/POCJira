using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POCJira.Shared.Models;

namespace POCJira.Repository.Interfaces
{
    public interface ICardsRepository
    {
        Task<IEnumerable<ChamadoModel>> GetAllCards();

        Task UpdateSqlCard(UpdateSqlModel input);
    }
}
