using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Exceptions
{
    /// <summary>
    /// Dans le cas où l'utilisateur n'a pas l'accès
    /// 
    /// logique serveur interdit l'acces à cette action pour cet utilisateur
    /// </summary>
    public class ForbiddenAccessException : Exception, INotLoggedException
    {
        /// <summary>
        /// Constructeur de base permettant de refuser l'accès pour l'utilisateur qui reçois cet exception.
        /// </summary>
        public ForbiddenAccessException() : base() { }
    }
}
