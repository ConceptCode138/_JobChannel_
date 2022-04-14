using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests
{
    public class TypePosteDTORequest
    {
        /// <summary>
        /// Identity of 'PosteID' -> TypePosteDTORequest
        /// </summary>
        //public int PosteID { get; set; }

        /// <summary>
        /// Correspond to the "PosteName > TypePosteDTORequest
        /// </summary>
        public string PosteName { get; set; }

        /// <summary>
        /// This method allow to create a new instance of TypePoste.
        /// </summary>
        /// <returns>return a new object TypePoste</returns>
        public TypePoste ToTypePoste()
        {
            return new TypePoste()
            {
                //IdPoste       = PosteID,
                IntitulePoste = PosteName
            };
        }
    }
}
