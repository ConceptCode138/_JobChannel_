using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    /// <summary>
    /// Représente le type de poste
    /// </summary>
    public class TypePoste : Entity 
    {
        #region PROPERTIES
        /// <summary>
        /// Identifiant du type de poste
        /// </summary>
        public int IdPoste { get; set; }
        /// <summary>
        /// type de poste
        /// </summary>
        public string IntitulePoste { get; set; }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public TypePoste() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typePoste"></param>
        public TypePoste(int idPoste, string typePoste)
        {
            IdPoste       = idPoste;
            IntitulePoste = typePoste;
        }
        #endregion
    }
}
