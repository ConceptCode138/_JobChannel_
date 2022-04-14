using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests
{
    class CreateTypePosteDTORequest
    {
        /// <summary>
        /// Correspond to the "IntitulePoste"
        /// </summary>
        public string PosteType { get; set; }

        /// <summary>
        /// This method allow to create a new instance of TypePoste
        /// </summary>
        /// <returns>return a new object of TypePoste</returns>
        public TypePoste ToPoste()
        {
            return new TypePoste()
            {
                IntitulePoste = PosteType
            };
        }
    }
}
