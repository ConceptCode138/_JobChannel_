using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW
{
    internal class DbSession : IDbSession
    {
        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; set; }

        /// <summary>
        /// A la création de la DbSession, récupération de la configuration du serveur se trouvant dans API -> appsettings.json -> connectionString de la base de données
        /// </summary>
        public DbSession(IConfiguration configuration)
        {
            //récupération de la chaine de connexion par défaut SQLServer
            var connectionString = configuration.GetConnectionString("Default");
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();
        }

        /// <summary>
        /// Si plus d'utilité de la connexion, fermeture de celle-ci
        /// </summary>
        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
