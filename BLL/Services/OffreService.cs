using BLL.Interfaces;
using BO.DTO.DTORequests.FiltersDTO;
using BO.DTO.DTOResponses;
using BO.Entities;
using BO.Exceptions;
using DAL.UOW;
using DAL.UOW.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OffreService : IOffreService
    {
        #region CONSTANTES
        private string PATH = @"C:\Users\cda4rive\OneDrive - AMIO\ACTIVITE - 3\JALON - 4\ProjetCSharpFR\traceJobChannel.txt";
        #endregion

        private readonly ITraceService _traceService;
        private readonly IUnitOfWork _uow;

        private readonly ILogger<OffreService> _logger;

        #region CONSTRUCTORS

        public OffreService(ITraceService traceService, IUnitOfWork uow, ILogger<OffreService> logger)
        {
            _traceService = traceService;
            _uow          = uow;
            _logger       = logger;
        }
        #endregion

        #region LOGIQUE METIER

        #region GET - LIST OFFERS
        /// <summary>
        /// Retrieve the list offers
        /// </summary>
        /// <returns>return an offer collection</returns>
        public async Task<IEnumerable<Offre>> GetAllOffresInCatalogAsync(PageDTOWithFilters pagination)
        {
            //before action
            _logger.LogDebug($"-> GetAllOffresInCatalogAsync() <- RETRIEVE ALL OFFERS");

            //action
            // récupération du répertoire des offres grace à l'UOW, afin d'appeler la méthode permettant de récupérer toutes les offres.
            var offerRepository = _uow.GetRepository<IOffreRepository>();

            IEnumerable<Offre> offers;
            //appel de la méthode pour récupérer la liste d'offres avec une limite des dix dernières offres.
            if (pagination.Page == 0 && pagination.NbDataPerPage == 0 )
            {
                offers = await offerRepository.FindAllAsync();
            }
            else
            {
                offers = await offerRepository.FindAllWithFilter(pagination);
            }

            if ( File.Exists(PATH) && offers.Count() > 0 )
            {
                //Add a track on file text previously created
                using (StreamWriter streamWriter = File.AppendText(PATH))
                {
                    streamWriter.WriteLine(_traceService.TraceOnFileTXT($"a récupéré {offers.Count()} offre(s)"));
                }
            }

            _traceService.TraceOnServer($"-> {offers.Count()} offre(s) récupérée(s)...");

            return (offers is not null) ? offers : throw new NotFoundException("Aucune offre n'a été trouvée");
        }
        #endregion

        public async Task<Offre> CreateOffreAsync(Offre offre)
        {
            _logger.LogDebug($"-> CreateOffreAsync() <- CREATION OF A CURRENT OFFER");

            //action
            //retrieve the repertory of offers with UOW in order to call the method allowing to add a new offer.
            var offerRepository    = _uow.GetRepository<IOffreRepository>();
            var jobRepository      = _uow.GetRepository<ITypePosteRepository>();
            var contractRepository = _uow.GetRepository<ITypeContratRepository>();

            //Call this method for inserting a new offer.
            var newOffer = await offerRepository.ADDAsync(offre);

            if (newOffer != null)
            {
                //Add a track on file text previously created
                using (StreamWriter streamWriter = File.AppendText(PATH))
                {
                    streamWriter.WriteLine(_traceService.TraceOnFileTXT($"a ajouté une nouvelle offre portant l'id: {newOffer.IdOffre}"));
                }

                return newOffer;
            }
            else
            {
                throw new NotFoundException($"{nameof(newOffer)} pour id ({newOffer.IdOffre})");
            }
        }

        public async Task DeleteOffreAsync(int idOffre)
        {
            //before action
            _logger.LogDebug($"-> DeleteOffreAsync(int idOffre) <- DELETING OF A CURRENT OFFER");

            //action
            //retrieve the repertory of offers with UOW in order to call the method allowing to delete an offer.
            var offerRepository = _uow.GetRepository<IOffreRepository>();

            //Call this method for deleting an offer.
            var offer = await offerRepository.DeleteAsync(idOffre);

            if (offer == 0)
            {
                throw new NotFoundException($"{nameof(Offre)} with id ({idOffre}) wasn't exist");
            }
            else
            {
                if (File.Exists(PATH))
                {
                    //Add a track on file text previously created
                    using (StreamWriter streamWriter = File.AppendText(PATH))
                    {
                        streamWriter.WriteLine(_traceService.TraceOnFileTXT($"SUPPRESSION offre numéro: {idOffre}"));
                    }
                }
            }
        }




        /// <summary>
        /// Retrieve the offer by the Id.
        /// </summary>
        /// <param name="idOffre">id of offer to retrieve</param>
        /// <returns>return an offer</returns>
        public async Task<Offre> GetOffreByIDAsync(int idOffre)
        {
            //before action
            _traceService.TraceOnServer($"Récupération de l'offre : {idOffre} en cours...");

            //// Research in DB
            //Offre offer = await Task.FromResult(offresDB.Find(offre => offre.IdOffre == idOffre));

            // récupération du répertoire des offres grace UOW, afin d'appeler la méthode permettant de récupérer toutes les offres.
            var offerRepository = _uow.GetRepository<IOffreRepository>();

            //appel de la méthode pour récupérer la liste d'offres.
            var offer = await offerRepository.GetByIdAsync(idOffre);

            if (offer is null)
            {
                throw new NotFoundException(nameof(offer), new { Id = idOffre });
            }
            else
            {
                //Add a track on file text previously created
                using (StreamWriter streamWriter = File.AppendText(PATH))
                {
                    streamWriter.WriteLine(_traceService.TraceOnFileTXT($"a récupéré l'offre :{offer.IdOffre} -> {offer.Title}"));
                }

                _traceService.TraceOnServer($"-> L'offre :{offer.IdOffre} a été récupérée...");

                return offer;
            }
        }

        /// <summary>
        /// Allow to modify the resource
        /// </summary>
        /// <param name="offre"></param>
        /// <returns></returns>
        public async Task<Offre> UpdateOffreAsync(Offre offre)
        {
            //action
            //retrieve the repertory of offers with UOW in order to call the method allowing to modify an offer.
            var offerRepository   = _uow.GetRepository<IOffreRepository>()    ;
            var typeJobRepository = _uow.GetRepository<ITypePosteRepository>();

            var jobRetrieved = await typeJobRepository.GetByIdAsync(offre.IdTypePoste);  //     AWAIT

            if (jobRetrieved is null)
            {
                throw new NotFoundException($"{nameof(jobRetrieved)} cet identifiant de type poste n'a pas été trouvé ");
            }



            //Call this method for modify an offer.
            var updatedOffer = await offerRepository.ModifyAsync(offre);

            if (updatedOffer != null)
            {
                //mise à jour des valeurs
                // ajout de texte du fichier deja existant.
                using (StreamWriter streamWriter = File.AppendText(PATH))
                {
                    streamWriter.WriteLine(_traceService.TraceOnFileTXT($" -> Modification de l'offre numéro: {updatedOffer.IdOffre}"));
                }
                //renvoie de la ressource modifiée
                return updatedOffer;
            }
            else
            {
                throw new NotFoundException($"{nameof(updatedOffer)} pour id ({updatedOffer.IdOffre})");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TypeContrat>> GetAllTypeContratsInOfferCatalogAsync()
        {
            //before action
            _traceService.TraceOnServer("Récupération de la liste de type de contrat en cours...");

            //action
            // récupération du répertoire des offres grace UOW, afin d'appeler la méthode permettant de récupérer tous les types de contrats présent dans les offres.
            var contratRepository = _uow.GetRepository<ITypeContratRepository>();

            //appel de la méthode pour récupérer la liste de type de contrats.
            var contractTypes = await contratRepository.FindAllAsync();

            var listContracts = new List<TypeContrat>();

            foreach (var contract in contractTypes)
            {
                listContracts.Add(contract);
            }

            if (File.Exists(PATH))
            {
                //Add a track on file text previously created
                using (StreamWriter streamWriter = File.AppendText(PATH))
                {
                    streamWriter.WriteLine(_traceService.TraceOnFileTXT($"a récupéré {listContracts.Count} type(s) de contrat"));
                }
            }

            _traceService.TraceOnServer($"-> {listContracts.Count} type(s) de contrat récupéré(s)...");

            return listContracts;


        }



        #endregion



        #region ----------------------------!!!!!   TEST A SUPPRIMER   !!!!!------------------------------
        public static List<TypeContrat> TypeContratsDB { get; set; } = new List<TypeContrat>()
        {
            new TypeContrat(1,"CDD"),
            new TypeContrat(2,"CDI"),
            new TypeContrat(3,"Contrat de professionnalisation"),
            new TypeContrat(4,"Stage"),
            new TypeContrat(5,"Alternance"),
            new TypeContrat(6,"Intérim")
        };
        public static List<TypePoste> TypePostesDB { get; set; } = new List<TypePoste>()
        {
            new TypePoste(1,"Développeur"),
            new TypePoste(2,"Développeur web"),
            new TypePoste(3,"Développeur backend"),
            new TypePoste(4,"Développeur fullstack"),
            new TypePoste(5,"DevOps"),
            new TypePoste(6,"Technicien réseau"),
            new TypePoste(7,"Technicien informatique")
        };
        public static List<Region> RegionsDB { get; set; } = new List<Region>()
        {
            new Region(1,"Languedoc-Roussillon"),
            new Region(2,"Midi-Pyrénnées"),
            new Region(3,"Aquitaine"),
            new Region(4,"Poitou-Charentes"),
            new Region(5,"Pays de la loire"),
            new Region(6,"Bretagne"),
            new Region(7,"Basse-Normandie"),
            new Region(8,"Haute-Normandie"),
            new Region(9,"Nord-Pas-De-Calais"),
            new Region(10,"Picardie"),
            new Region(11,"Champagne-Ardenne"),
            new Region(12,"Lorraine"),
            new Region(13,"Alsace"),
            new Region(14,"Franche-Comté"),
            new Region(15,"Rhône-Alpes"),
            new Region(16,"Provence-Alpes-Côte d'Azur"),
            new Region(17,"Auvergne"),
            new Region(18,"Limousin"),
            new Region(19,"Centre-Val De Loire"),
            new Region(20,"Ile-De-France"),
            new Region(21,"Bourgogne"),
            new Region(22,"Corse")
        };
        public static List<Offre> offresDB { get; set; } = new List<Offre>()
        {
            new Offre(1,"Recherche Développeur/se","https://google.com","recherche un développeur gratuit", TypePostesDB[0], TypeContratsDB[3], RegionsDB[0]),
            new Offre(2,"Recherche Développeur/se","https://google.com","recherche un développeur gratuit", TypePostesDB[0], TypeContratsDB[1], RegionsDB[19]),
            new Offre(3,"Recherche Informaticien/ne",null,"recherche un Informaticien gratuit", TypePostesDB[6], TypeContratsDB[3], RegionsDB[1]),
            new Offre(4,"Recherche Développeur/se","https://google.com","recherche un développeur gratuit", TypePostesDB[0], TypeContratsDB[4], RegionsDB[2]),
            new Offre(5,"Recherche Développeur/se","https://google.com","recherche un développeur gratuit", TypePostesDB[0], TypeContratsDB[2], RegionsDB[17]),
            new Offre(6,"Recherche Développeur/se","https://google.com","recherche un développeur gratuit", TypePostesDB[0], TypeContratsDB[4], RegionsDB[11]),
            new Offre(7,"Recherche Développeur/se","https://google.com","recherche un développeur gratuit", TypePostesDB[0], TypeContratsDB[4], RegionsDB[21]),
            new Offre(8,"Recherche Développeur/se","https://google.com","recherche un développeur gratuit", TypePostesDB[0], TypeContratsDB[3], RegionsDB[5])
        };
        #endregion



    }
}
