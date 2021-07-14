using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using POCJira.Shared.Contexts.Interfaces;
using POCJira.Repository.Entities;
using POCJira.Repository.Interfaces;
using POCJira.Repository.Repositories.Base;
using POCJira.Shared.Models;
using System.Data.SqlClient;
using Dapper;
using System.Net.Http;

namespace POCJira.Repository.Repositories
{
    public class CardsRepository : DbRepositoryBase<ChamadoEntity>, ICardsRepository
    {
        private readonly IMapper mapper;
        public CardsRepository(IDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ChamadoModel>> GetAllCards()
        {
            var sql = @$"SELECT * FROM Chamado";

            var result = await QueryAsync<ChamadoEntity>(sql, null);

            return mapper.Map<IEnumerable<ChamadoEntity>, IEnumerable<ChamadoModel>>(result);
        }

        public async Task UpdateSqlCard(UpdateSqlModel input)
        {
            var sql = $@"UPDATE Chamado
                    SET status=@status,
                        nome=@nome,
                        descricao=@descricao,
                        valor=@valor,
                        responsavel=@responsavel
                    WHERE IdCardJira = @idCardJira";

            var parameters = new DynamicParameters();

            parameters.Add("@idCardJira", input.Issue.Id, System.Data.DbType.Int64);
            parameters.Add("@status", input.Issue.Fields.Status.Name, System.Data.DbType.String);
            parameters.Add("@nome", input.Issue.Fields.Summary, System.Data.DbType.String);
            parameters.Add("@descricao", input.Issue.Fields.Description, System.Data.DbType.String);
            parameters.Add("@valor", input.Issue.Fields.CustomField_10030, System.Data.DbType.Decimal);
            parameters.Add("@responsavel", input.Issue.Fields.Assignee?.DisplayName, System.Data.DbType.String);

            await ExecuteAsync(sql, parameters);
        }
    }
}
