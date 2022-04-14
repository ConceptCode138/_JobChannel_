using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests
{
    public class TypeContratDTORequest
    {
        /// <summary>
        /// Identity of 'ContratID' -> TypeContratDTORequest
        /// </summary>
        //public int ContratID { get; set; }

        /// <summary>
        /// Correspond to the "ContratName > TypeContratDTORequest
        /// </summary>
        public string ContratName { get; set; }

        /// <summary>
        /// This method allow to create a new instance of TypeContrat.
        /// </summary>
        /// <returns>return a new object TypeContrat</returns>
        public TypeContrat ToTypeContrat()
        {
            return new TypeContrat()
            {
                //IdContrat       = ContratID,
                IntituleContrat = ContratName
            };
        }
    }
}
