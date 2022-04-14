using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    /// <summary>
    /// Représente une offre
    /// </summary>
    public class Offre : Entity
    {
        #region PROPERTIES
        /// <summary>
        /// identifiant de l'offre
        /// </summary>
        public int IdOffre { get; set; }
        /// <summary>
        /// Titre de l'offre
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Lien url de l'offre
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Description de l'offre
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Date de publication de l'offre
        /// </summary>
        public DateTime DtCreated { get; set; }
        /// <summary>
        /// Type de poste de l'offre (Développeur)
        /// </summary>
        public TypePoste Poste { get; set; }
        /// <summary>
        /// Localisation de l'offre correspondant à la region
        /// </summary>
        public Region Localisation { get; set; }
        /// <summary>
        /// Type de contrat de l'offre (CDD,CDI, Contrat de professionnalisation)
        /// </summary>
        public TypeContrat Contrat { get; set; }

        public int IdTypePoste { get; set; }

        public int IdTypeContrat { get; set; }

        public int IdRegionName { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructeur par défaut afin d'éviter de levé une exception
        /// </summary>
        public Offre() { }


        /// <summary>
        /// Constructeur de l'offre
        /// </summary>
        /// <param name="titre">titre offre</param>
        /// <param name="lien">lien url offre</param>
        /// <param name="description">description de l'offre</param>
        /// <param name="typePoste">type de poste de l'offe</param>
        /// <param name="typeContrat">type de contrat de l'offre</param>
        /// <param name="region">région de l'offre</param>
        public Offre(int idOffre,string titre, string lien, string description, TypePoste typePoste, TypeContrat typeContrat, Region region)
        {
            IdOffre     = idOffre;
            Title       = titre;
            Link        = lien;
            Description = description;
            Poste       = typePoste  ;
            Contrat     = typeContrat;
            Localisation= region;
        }

        #endregion
    }
}
