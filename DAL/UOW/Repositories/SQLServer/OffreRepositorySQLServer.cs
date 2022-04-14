using BO.DTO.DTORequests.FiltersDTO;
using BO.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;// driver SQL Server
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using BO.DTO.DTORequests;
using System.Dynamic;

namespace DAL.UOW.Repositories.SQLServer
{
    /// <summary>
    /// Implémentation pour SQL SERVER
    /// </summary>
    internal class OffreRepositorySQLServer : IOffreRepository
    {
        // utilisation de DbSession afin de pouvoir executer les requêtes.
        private readonly IDbSession _session;

        /// <summary>
        /// Ce constructeur a besoin de cet DbSession afin d'executer les requêtes.
        /// </summary>
        /// <param name="dbSession"></param>
        public OffreRepositorySQLServer(IDbSession dbSession)
        {
            _session = dbSession;
        }



        public async Task<IEnumerable<Offre>> FindAllAsync()                                                                                //var stmt = @"select * from book ORDER BY Id OFFSET @PageSize * (@Page - 1) rows FETCH NEXT @PageSize rows only";
        {
            // utilisation de la methode d'extension QueryAsync de la librairie Dapper / dans lequel se trouve le mapping qui est effectué.

            const string query = @"SELECT TOP(10)Offre.dtCreated,
                                          Offre.IdOffre,
                                          Offre.title,
                                          Offre.description,
                                          Offre.link,
                                          Offre.dtCreated, 
                                          Offre.IdRegion, Region.intituleRegion,
                                          Offre.IdContrat, TypeContrat.intituleContrat,
                                          Offre.IdPoste, TypePoste.intitulePoste

                                   FROM Offre INNER JOIN Region      ON Offre.IdRegion  = Region.IdRegion 
                                              INNER JOIN TypeContrat ON Offre.IdContrat = TypeContrat.IdContrat 
                                              INNER JOIN TypePoste   ON Offre.IdPoste   = TypePoste.IdPoste
                                   ORDER BY Offre.dtCreated DESC";


            var test =  await _session.Connection.QueryAsync<Offre, Region, TypeContrat, TypePoste, Offre>(query, (offre, region, typeContrat, typePoste) => 
            {
                offre.Localisation = region     ;
                offre.Contrat      = typeContrat;
                offre.Poste        = typePoste  ;

                return offre;
            }, 
            splitOn:"IdRegion, IdContrat, IdPoste",  transaction:_session.Transaction);

            return test;

            #region WITH ADO.NET

            ////query string without parameter but with the columns identified to limit any problems with a change in a column.
            //string queryString = "SELECT IdOffre, description, lien, titre FROM offre;";

            ////create a new command sql with query and connection.
            //SqlCommand command = new SqlCommand(queryString, (SqlConnection)_session.Connection);

            ////Execute the command.
            //var readerAllOffers = await command.ExecuteReaderAsync();

            //List<Offre> result = new List<Offre>();

            ////read in the reader
            //while (readerAllOffers.Read())
            //{
            //    //new instance 'offre'
            //    var offer = new Offre()
            //    {
            //        IdOffre     = readerAllOffers.GetInt32(0),
            //        Description = readerAllOffers.GetString(1),
            //        Lien        = readerAllOffers.GetString(2),
            //        Titre       = readerAllOffers.GetString(3)
            //    };
            //    //add a new object in the list.
            //    result.Add(offer);
            //}
            ////return the list offers
            //return result;
            #endregion
        }

        public async Task<Offre> GetByIdAsync(int id)
        {

            const string query = @"SELECT *  FROM Offre INNER JOIN Region      ON Offre.IdRegion  = Region.IdRegion 
                                                        INNER JOIN TypeContrat ON Offre.IdContrat = TypeContrat.IdContrat 
                                                        INNER JOIN TypePoste   ON Offre.IdPoste   = TypePoste.IdPoste
                                   WHERE Offre.IdOffre = @Id";


            var test = (await _session.Connection.QueryAsync<Offre, Region, TypeContrat, TypePoste, Offre>(query, (offre, region, typeContrat, typePoste) =>
            {
                offre.Localisation = region;
                offre.Contrat = typeContrat;
                offre.Poste = typePoste;

                return offre;

            }, new { Id = id}, splitOn: "IdRegion, IdContrat, IdPoste", transaction: _session.Transaction)).FirstOrDefault();

            return test;

        }

        public async Task<Offre> ADDAsync(Offre offre)
        {


            const string queryInsert = @"INSERT INTO Offre (title, description, dtCreated, link, IdPoste, IdContrat, IdRegion)
                                           OUTPUT INSERTED.IdOffre
                                                    VALUES (@title, @description, @dtCreated, @link, @IdPoste, @IdContrat, @IdRegion)";


            int ID = await _session.Connection.QuerySingleAsync<int>(queryInsert, new
            {
                title       = offre.Title,
                description = offre.Description,
                dtCreated   = offre.DtCreated,
                link        = offre.Link,
                IdPoste     = offre.Poste.IdPoste,
                IdContrat   = offre.Contrat.IdContrat,
                IdRegion    = offre.Localisation.IdRegion,

            }, transaction: _session.Transaction);

            var offreCreated = await GetByIdAsync(ID);

            return offreCreated;


        }

        public async Task<Offre> ModifyAsync(Offre offre)
        {
            const string QUERY_UPDATE = @"UPDATE Offre
                                          SET   title = @title,
                                                description = @description,
                                                link        = @link,
                                                IdPoste     = @IdPoste,
                                                IdContrat   = @IdContrat,
                                                IdRegion    = @IdRegion
                                          WHERE IdOffre = @IdOffre;";

            int idOffreModif = await _session.Connection.ExecuteAsync(QUERY_UPDATE, new
            {
                title       = offre.Title        ,
                description = offre.Description  ,
                link        = offre.Link         ,
                IdPoste     = offre.IdTypePoste  ,
                IdContrat   = offre.IdTypeContrat,
                IdRegion    = offre.IdRegionName ,
                IdOffre     = offre.IdOffre

            }, transaction: _session.Transaction);

            var updatedOffer = await GetByIdAsync(offre.IdOffre);

            return updatedOffer;

        }



        public async Task<int> DeleteAsync(int idEntity)
        {
            const string deleteString = @"DELETE FROM Offre WHERE IdOffre = @IdOffre";

            var deleting = (await _session.Connection.ExecuteAsync(deleteString, new { IdOffre = idEntity }, _session.Transaction));

            return deleting;
        }









        public  Task<Offre> GetByTypeContratIdAsync(int idTypeContrat)
        {
            throw new NotImplementedException();
            //return await _session.Connection.QueryAsync<Offre, TypeContrat>(@"SELECT o.IdOffre, o.description, o.lien, o.titre FROM offre o JOIN typeContrat c ON c.IdContrat = o.IdContrat WHERE c.IdContrat = @ID;", new { ID = idTypeContrat }, _session.Transaction);
        }

        public async Task<IEnumerable<Offre>> FindAllWithFilter(PageDTOWithFilters pageDTOWithFilters)
        {
            dynamic ValueOfParam  = new ExpandoObject();
            ValueOfParam.PageSize = pageDTOWithFilters.NbDataPerPage;
            ValueOfParam.Page     = pageDTOWithFilters.Page;

            var stmt = this.ClauseWhere(pageDTOWithFilters, ValueOfParam);
            // http://localhost:5000/api/offre?Poste.PosteName=Developpeur_fullstack&Contrat.ContratName=CDD&Region.RegionName=Midi-Pyrennees&MinDate=2022/03/02&MaxDate=2022/04/01&Page=1&NbDataPerPage=20
            var clause = (stmt != String.Empty) ? stmt : String.Empty;

            // utilisation de la methode d'extension QueryAsync de la librairie Dapper / dans lequel se trouve le mapping qui est effectué.
            string query = @$"SELECT *  FROM Offre INNER JOIN Region      ON Offre.IdRegion  = Region.IdRegion 
                                                   INNER JOIN TypeContrat ON Offre.IdContrat = TypeContrat.IdContrat 
                                                   INNER JOIN TypePoste   ON Offre.IdPoste   = TypePoste.IdPoste
                                   {clause}
                                   ORDER BY IdOffre
                                   OFFSET @PageSize * ( @Page - 1 ) rows
                                   FETCH NEXT @PageSize rows only";


            var test = await _session.Connection.QueryAsync<Offre, Region, TypeContrat, TypePoste, Offre>(query, (offre, region, typeContrat, typePoste) =>
            {
                offre.Localisation = region;
                offre.Contrat      = typeContrat;
                offre.Poste        = typePoste;

                return offre;
            },
            ValueOfParam as ExpandoObject,
            splitOn: "IdRegion, IdContrat, IdPoste", transaction: _session.Transaction);

            return test;
        }

        private List<object> GetAttributeOfFilter(PageDTOWithFilters pageDTOWithFilters)
        {
            List<object> attrOfFilter = new List<object>();

            foreach (PropertyInfo attribute in pageDTOWithFilters.GetType().GetProperties())
            {
                attrOfFilter.Add(attribute.GetValue(pageDTOWithFilters));
            }

            return attrOfFilter;
        }



        private string ClauseWhere(PageDTOWithFilters pageDTOWithFilters, ExpandoObject valueOfParam)
        {
            Dictionary<string, string> valueOfAttribute = new Dictionary<string, string>();

            // Récupération du type "PageDTOWithFilters"
            Type type = pageDTOWithFilters.GetType();

            // Attributs de la classe du type "PageDTOWithFilters"
            List<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());

            foreach (PropertyInfo prop in props)
            {
                // Récupération de la valeur de l'attribut de l'objet de la classe
                var valueAttributeObjectClass = prop.GetValue(pageDTOWithFilters);

                Debug.WriteLine(valueAttributeObjectClass);

                if (valueAttributeObjectClass is not null)
                {
                    // Si l'attribut de l'objet de la classe est un objet "Exemple : TypeContratDTORequest"
                    if (valueAttributeObjectClass is DateTime)
                    {
                        valueOfAttribute.Add(prop.Name, Convert.ToString(valueAttributeObjectClass));
                    }
                    else if (valueAttributeObjectClass is object)
                    {
                        var valuesAttributeOfAttributeObjectClass = valueAttributeObjectClass.GetType().GetProperties();
                        // Bouclage sur les attributs de l'objet
                        foreach (var item in valuesAttributeOfAttributeObjectClass)
                        {
                            valueOfAttribute.Add(prop.Name, Convert.ToString(item.GetValue(prop.GetValue(pageDTOWithFilters))));
                        }
                    }
                }
            }
            var clause = BuilderClauseDynamic(valueOfAttribute, valueOfParam);

            return clause;
        }

        private string BuilderClauseDynamic(Dictionary<string, string> valuesDico, ExpandoObject valueOfParam)
        {
            string queryClause = String.Empty;
            string betweenDT   = String.Empty;
            string param       = String.Empty;
            int counter        = 0;

            foreach (KeyValuePair<string, string> objAttribute in valuesDico)
            {
                counter++; 
                
                if (counter == 1)
                {
                    queryClause += "WHERE ";
                }

                if (objAttribute.Key == "Poste")
                {
                    param = "@intitulePoste";
                    (valueOfParam as ExpandoObject).TryAdd(param, objAttribute.Value);
                    queryClause += $"TypePoste.intitulePoste = {param}";
                }
                else if (objAttribute.Key == "Contrat")
                {
                    param = "@intituleContrat";
                    (valueOfParam as ExpandoObject).TryAdd(param, objAttribute.Value);
                    queryClause += $"TypeContrat.intituleContrat = {param}";
                }
                else if (objAttribute.Key == "Region")
                {
                    param = "@intituleRegion";
                    (valueOfParam as ExpandoObject).TryAdd(param, objAttribute.Value);
                    queryClause += $"Region.intituleRegion = {param}";
                }
                else if (objAttribute.Key == "MinDate")
                {
                    param = "@min";
                    (valueOfParam as ExpandoObject).TryAdd(param, objAttribute.Value);
                    betweenDT += $"Offre.dtCreated BETWEEN {param} AND ";
                }
                else if (objAttribute.Key == "MaxDate")
                {
                    param = "@max";
                    (valueOfParam as ExpandoObject).TryAdd(param, objAttribute.Value);
                    queryClause += $"{betweenDT}{param}";
                }

                if (counter < valuesDico.Count() && objAttribute.Key !="MinDate")
                {
                    queryClause += " AND ";
                }
            }

            return queryClause;
        }

    }
}
