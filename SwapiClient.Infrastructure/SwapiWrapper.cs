using Microsoft.Extensions.Logging;
using StarWarsApiCSharp;
using SwapiClient.Application;
using SwapiClient.Domain;

namespace SwapiClient.Infrastructure
{
    public class SwapiWrapper: ISwapiWrapper
    {
        //private readonly IRepository<Person> _personRepo;
        private readonly ISwapiHttpClientHelper _swapiHttpClientHelper;
        private readonly ILogger<SwapiWrapper> _logger;
        public SwapiWrapper(ISwapiHttpClientHelper swapiHttpClientHelper, ILogger<SwapiWrapper> logger)
        {
            _swapiHttpClientHelper = swapiHttpClientHelper;
            _logger = logger;
        }

        /// <summary>
        /// This method gets person's basic details
        /// </summary>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IList<PersonDetailedInfo> GetPersonsDetailedInfo(int pageNo = 1)
        {
            IList<PersonDetailedInfo> persons = new List<PersonDetailedInfo>();

            try
            {
                var personsTask = GetPersons(pageNo);
                
                persons = personsTask
                .Select(per => new PersonDetailedInfo
                {
                        Name = per.Name,
                        Gender = per.Gender,
                        Height = per.Height,
                        Mass = per.Mass,
                        BirthYear = per.BirthYear,
                        EyeColor = per.EyeColor,
                        SkinColor = per.SkinColor,
                        HairColor = per.HairColor,
                    })
                .ToList();
        }
            catch (Exception ex)
            {
                var message = "Error fetching Persons";
                _logger.LogError(ex, message);

                //Create Custom Exception if we would like to handle it with a specific Status Code.
                throw new Exception(message);
            }

            return persons;
        }

        /// <summary>
        /// this method gets the person's detailed info.
        /// </summary>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public IList<PersonBasicInfo> GetPersonsBasicInfo(int pageNo = 1)
        {
            IList<PersonBasicInfo> persons = new List<PersonBasicInfo>();

            try
            {
                var personsTask = GetPersons(pageNo);
                
                persons = personsTask
                .Select(per => new PersonBasicInfo
                {
                    Name = per.Name,
                    Gender = per.Gender,
                    Height = per.Height,
                    Mass = per.Mass,
                })
                .ToList();
            }
            catch (Exception ex)
            {
                var message = "Error fetching Persons";
                _logger.LogError(ex, message);

                //Create Custom Exception if we would like to handle it with a specific Status Code.
                throw new Exception(message);
            }
            

            return persons;
        }

        public ICollection<SwapiPeople> GetPersons(int pageNo = 1)
        {
            var peopleTask = _swapiHttpClientHelper.GetPeople(pageNo);
            Task.WaitAll(peopleTask.AsTask());

            return peopleTask.Result.Results;

        }
    }
}