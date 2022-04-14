using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITraceService
    {
        /// <summary>
        /// Permet de laisser des traces sur le serveur pour toute manipulation
        /// </summary>
        /// <param name="message"></param>
        void TraceOnServer(string message);

        /// <summary>
        /// Permet de laisser une trace écrite dans un fichier texte permettant ainsi de suivre toutes les manipulations.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>du texte dans un fichier</returns>
        string TraceOnFileTXT(string message);
    }
}
