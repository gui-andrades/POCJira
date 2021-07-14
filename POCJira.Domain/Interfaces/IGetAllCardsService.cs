using System.Collections.Generic;
using System.Threading.Tasks;
using POCJira.Shared.Models;

namespace POCJira.Domain.Interfaces
{
    public interface IGetAllCardsService
    {
        Task<IEnumerable<ChamadoModel>> GetAllCards();
    }
}
