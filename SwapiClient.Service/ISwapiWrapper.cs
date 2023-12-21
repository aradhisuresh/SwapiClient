using SwapiClient.Domain;

namespace SwapiClient.Application
{
    public interface ISwapiWrapper
    {
        IList<PersonBasicInfo> GetPersonsBasicInfo(int pageNo = 1);
        IList<PersonDetailedInfo> GetPersonsDetailedInfo(int pageNo = 1);
    }
}