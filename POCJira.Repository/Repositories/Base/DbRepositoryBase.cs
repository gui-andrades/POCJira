using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using POCJira.Shared.Contexts.Interfaces;

namespace POCJira.Repository.Repositories.Base
{
    public abstract class DbRepositoryBase<TEntity>
    {
        public IDbContext Context { get; protected set; }

        protected DbRepositoryBase(IDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters)
        {
            var result = await Context.Connection.QueryAsync<T>(sql, parameters, Context.Transaction, null, null);

            return result;
        }

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters, Type[] types, Func<object[], T> map, string splitOn)
        {
            return await Context.Connection.QueryAsync<T>(
                sql: sql,
                param: parameters,
                transaction: Context.Transaction,
                types: types,
                commandTimeout: null,
                commandType: null,
                map: map,
                splitOn: splitOn).ConfigureAwait(false);
        }

        protected async Task<int> ExecuteAsync(string sql, object parameters)
        {
            return await Context.Connection.ExecuteAsync(sql, parameters, Context.Transaction, null, null);
        }
    }
}
