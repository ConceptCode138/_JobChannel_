using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPosteService
    {
        #region MyRegion

        #endregion
        /// <summary>
        /// Allow to retrieve the list of TypePoste
        /// </summary>
        /// <returns>return a collection of TypePoste</returns>
        Task<IEnumerable<TypePoste>> GetAllPostesAsync();

        /// <summary>
        /// Allow to retrieve a TypePoste by him ID
        /// </summary>
        /// <param name="idPoste"></param>
        /// <returns>return a TypePoste</returns>
        Task<TypePoste> GetPosteByIDAsync(int idPoste);

        /// <summary>
        /// Allow to create a new TypePoste in database
        /// </summary>
        /// <param name="poste"></param>
        /// <returns>return the TypePoste created</returns>
        Task<TypePoste> CreatePosteAsync(TypePoste poste);

        /// <summary>
        /// Allow to modify a TypePoste selected by him ID
        /// </summary>
        /// <param name="poste"></param>
        /// <returns>return the TypePoste updated</returns>
        Task<TypePoste> UpdatePosteAsync(TypePoste poste);

        /// <summary>
        /// Allow to delete a TypePoste selected by him ID
        /// </summary>
        /// <param name="idPoste"></param>
        /// <returns>return void</returns>
        Task DeletePosteAsync(int idPoste);
    }
}
