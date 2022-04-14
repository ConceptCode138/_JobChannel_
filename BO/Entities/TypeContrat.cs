using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    /// <summary>
    /// Représente le type de contrat
    /// </summary>
    public class TypeContrat : Entity
    {
        #region PROPERTIES
        /// <summary>
        /// Identifiant du type de contrat
        /// </summary>
        public int IdContrat { get; set; }
        /// <summary>
        /// type de contrat
        /// </summary>
        public string IntituleContrat { get; set; }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public TypeContrat() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idContrat"></param>
        /// <param name="typeContrat"></param>
        public TypeContrat(int idContrat, string typeContrat)
        {
            IdContrat       = idContrat;
            IntituleContrat = typeContrat;
        }
        #endregion
    }
}
