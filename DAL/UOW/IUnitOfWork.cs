using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW
{
    /// <summary>
    /// Cette interface dérive de l'interface IDisposable qui permet de ('disposer la connexion' et la remettre dans le pool de connexion)
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Permet de commencer une transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Permet d'envoyer la transaction apres manipulation des répertoires.
        /// </summary>
        void Commit();
        /// <summary>
        /// Permet d'annuler manuellement la transaction en cours.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Permet de demander un répertoire T (type inconnu) à l'appel de cette méthode, donner le type de repertoire souhaité.
        /// </summary>
        /// <typeparam name="T">Interface du répertoire</typeparam>
        /// <returns> retourne le répertoire demandé</returns>
        T GetRepository<T>();
    }
}
