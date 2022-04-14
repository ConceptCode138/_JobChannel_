using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Exceptions
{
    /// <summary>
    /// NotFoundException herite de Exception 
    /// NotFoundException herite de INotLoggedException uniquement pour trier les exceptions a logger ou non,
    /// ce qui est le cas ici dans le cas où un utilisateur cherche une offre qui n'existe plus, aucun intérêt particulier de logger sur le serveur.
    /// </summary>
    public class NotFoundException : Exception, INotLoggedException
    {
        /// <summary>
        /// Contructeur de base
        /// </summary>
        public NotFoundException() : base() { }

        /// <summary>
        /// Constructeur avec un message simple
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Constructeur avec un message et s'il y a une autre exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Constructeur le plus significatif
        /// </summary>
        /// <param name="name"> nom de l'objet</param>
        /// <param name="key">l'attribut de cet objet 'ID'</param>
        public NotFoundException(string name, object key) : base($"Entity {name} ({key}) was not found.") { }
    }
}
