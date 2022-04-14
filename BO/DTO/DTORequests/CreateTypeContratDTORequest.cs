using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests
{
    class CreateTypeContratDTORequest
    {
        #region PROPERTIES
        /// <summary>
        /// Type of contrat description
        /// </summary>
        public string ContratType { get; set; }
        #endregion


        public TypeContrat ToContrat()
        {
            return new TypeContrat()
            {
                IntituleContrat = ContratType
            };
        }
    }
}
