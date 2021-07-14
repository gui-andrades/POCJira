using System.Net.Mime;
using System.Data;
using System;
using POCJira.Shared.Contexts.Interfaces;
using POCJira.Shared.Configurations;
using System.Data.SqlClient;

namespace POCJira.Shared.Contexts.Base
{
    public class DbContext : IDbContext
    {
        public IDbConnection Connection { get; protected set; }

        public IDbTransaction Transaction { get; private set; }

        private int OpenTransactions { get; set; }

        public DbContext(Application application)
        {
            Connection = new SqlConnection(application.DbConnection);
        }

        public void CommitTransaction()
        {
            if (OpenTransactions == 0)
            {
                throw new Exception("Transação não inicializada");
            }
            OpenTransactions--;

            if (OpenTransactions != 0)
            {
                return;
            }
            Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            OpenTransactions = 0;
            Transaction.Rollback();
            Transaction.Dispose();

            Transaction = null;
        }

        public void BeginTransaction()
        {
            OpenTransactions++;

            if (OpenTransactions != 1)
            {
                return;
            }
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            Transaction = Connection.BeginTransaction();
        }

        protected virtual void DisposeAll()
        {
            if (Connection != null)
            {
                if (Transaction != null)
                {
                    if (Connection.State == ConnectionState.Open && OpenTransactions > 0)
                        Transaction.Rollback();

                    Transaction.Dispose();
                }
                Connection.Dispose();
            }
        }

        public void Dispose()
        {
            DisposeAll();
            GC.SuppressFinalize(this);
        }
    }
}
