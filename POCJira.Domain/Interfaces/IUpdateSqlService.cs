using System;
using System.Threading.Tasks;
using POCJira.Shared.Models;

namespace POCJira.Domain.Interfaces
{
    public interface IUpdateSqlService
    {
        Task UpdateSqlCard(UpdateSqlModel input);
    }
}
