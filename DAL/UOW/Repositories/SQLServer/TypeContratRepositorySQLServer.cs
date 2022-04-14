using BO.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW.Repositories.SQLServer
{
    class TypeContratRepositorySQLServer : ITypeContratRepository
    {

        // utilisation de DbSession afin de pouvoir executer les requêtes.
        private readonly IDbSession _session;

        /// <summary>
        /// Ce constructeur a besoin de cet DbSession afin d'executer les requêtes.
        /// </summary>
        /// <param name="dbSession"></param>
        public TypeContratRepositorySQLServer(IDbSession dbSession)
        {
            _session = dbSession;
        }


        public async Task<IEnumerable<TypeContrat>> FindAllAsync()
        {
            const string QUERY_GET = @"SELECT * FROM TypeContrat";

            var contrats = await _session.Connection.QueryAsync< TypeContrat>(QUERY_GET, transaction: _session.Transaction);

            return contrats;
        }

        public async Task<TypeContrat> GetByIdAsync(int id)
        {
            const string QUERY = @"SELECT * 
                                   FROM TypeContrat 
                                   WHERE IdContrat = @IdContrat";

            var contract = (await _session.Connection.QuerySingleAsync<TypeContrat>(QUERY, new { IdContrat = id }, transaction: _session.Transaction));

            return contract;
        }

        public async Task<TypeContrat> ADDAsync(TypeContrat typeContrat)
        {
            const string QUERY_INSERT = @"INSERT INTO TypeContrat (intituleContrat)
                                           OUTPUT INSERTED.IdContrat
                                                    VALUES (@intituleContrat)";


            int ID = await _session.Connection.QuerySingleAsync<int>(QUERY_INSERT, new
            {
                intituleContrat = typeContrat.IntituleContrat

            }, transaction: _session.Transaction);

            var contractTypeCreated = await GetByIdAsync(ID);

            return contractTypeCreated;
        }

        public async Task<int> DeleteAsync(int idTypeContrat)
        {
            const string DELETE_QUERY = @"DELETE FROM TypeContrat WHERE IdContrat = @IdContrat";

            var deleting = (await _session.Connection.ExecuteAsync(DELETE_QUERY, new { IdContrat = idTypeContrat }, _session.Transaction));

            return deleting;
        }

        public Task<TypeContrat> ModifyAsync(TypeContrat entity)
        {
            throw new NotImplementedException();
        }
    }
}
