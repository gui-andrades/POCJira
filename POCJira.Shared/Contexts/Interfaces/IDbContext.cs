using System.Data;
using System;

namespace POCJira.Shared.Contexts.Interfaces
{
    public interface IDbContext : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void CommitTransaction();

        void RollbackTransaction();

        void BeginTransaction();
    }
}
