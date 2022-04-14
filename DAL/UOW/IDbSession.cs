using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW
{
    /// <summary>
    /// Gère la connexion
    /// 
    /// Cette classe devra avoir une durée de vie de la requête (afin d'éviter que deux clients aient la même transaction)
    /// 
    /// IDbSession dérive de IDisposable permettant de fermer et libérer connexion. 
    /// </summary>
    public interface IDbSession : IDisposable
    {
        /// <summary>
        /// Connexion à la base de données
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// Transaction
        /// </summary>
        IDbTransaction Transaction { get; set; }
    }
}
