using BO.DTO.DTORequests.FiltersDTO;
using BO.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW.Repositories.SQLServer
{
    internal class TypePosteRepositorySQLServer : ITypePosteRepository
    {
        private readonly IDbSession _dbSession;


        public TypePosteRepositorySQLServer(IDbSession dbSession)
        {
            _dbSession = dbSession;
        }


        public async Task<TypePoste> ADDAsync(TypePoste typePoste)
        {
            const string QUERY_INSERT = @"INSERT INTO TypePoste (intitulePoste)
                                           OUTPUT INSERTED.IdPoste
                                                    VALUES (@intitulePoste)";


            int ID = await _dbSession.Connection.QuerySingleAsync<int>(QUERY_INSERT, new
            {
                intitulePoste = typePoste.IntitulePoste

            }, transaction: _dbSession.Transaction);

            var jobTypeCreated = await GetByIdAsync(ID);

            return jobTypeCreated;
        }

        public async Task<int> DeleteAsync(int idTypePoste)
        {
            const string DELETE_QUERY = @"DELETE FROM TypePoste WHERE IdPoste = @IdPoste";

            var deleting = (await _dbSession.Connection.ExecuteAsync(DELETE_QUERY, new { IdPoste = idTypePoste }, _dbSession.Transaction));

            return deleting;
        }


        public async Task<IEnumerable<TypePoste>> FindAllAsync()
        {
            const string QUERY = @"SELECT * FROM TypePoste";

            var postes = await _dbSession.Connection.QueryAsync<TypePoste>(QUERY, transaction: _dbSession.Transaction);

            return postes;
        }


        public async Task<TypePoste> GetByIdAsync(int id)
        {
            const string QUERY = @"SELECT * 
                                   FROM TypePoste 
                                   WHERE IdPoste = @IdPoste";

            var poste = (await _dbSession.Connection.QueryAsync<TypePoste>(QUERY, new { IdPoste = id }, transaction: _dbSession.Transaction)).FirstOrDefault();

            return poste;
        }

        public async Task<TypePoste> GetPosteByIntituleAsync(string posteIntitule)
        {
            const string QUERY = @"SELECT *  FROM TypePoste WHERE intitulePoste = @poste";

            var test = (await _dbSession.Connection.QuerySingleAsync<TypePoste>(QUERY, new { poste = posteIntitule }, transaction: _dbSession.Transaction));

            return test;
        }

        public Task<TypePoste> ModifyAsync(TypePoste entity)
        {
            throw new NotImplementedException();
        } // -> Not emplemented
    }
}
