using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    /// <summary>
    /// Représente une région
    /// </summary>
    public class Region : Entity
    {
        #region PROPERTIES
        /// <summary>
        /// Identifiant de la région
        /// </summary>
        public int IdRegion { get; set; }
        /// <summary>
        /// Intitulé région correspond à la région.
        /// </summary>
        public string IntituleRegion { get; set; }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Region() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="region"></param>
        public Region(int idRegion,string region)
        {
            IdRegion = idRegion;
            IntituleRegion = region;
        }
        #endregion
    }
}
